﻿using _0.Template_NET_Core.Common.Options;
using _1.Template_NET_Core.Application.Controllers.Validators;
using _1.Template_NET_Core.Application.Parameters;
using _1.Template_NET_Core.Application.ViewModels;
using _2.Template_NET_Core.Services.Implement;
using _2.Template_NET_Core.Services.Implements;
using _2.Template_NET_Core.Services.InfoModels;
using _2.Template_NET_Core.Services.Interface;
using _3.Template_NET_Core.Repositories.Conditions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace _1.Template_NET_Core.Application.Controllers
{
    /// <summary>
    /// 登入
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger<SampleController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IOptions<JwtSettingsOptions> _options;

        public AuthController(IMapper mapper, ILogger<SampleController> logger, IHttpContextAccessor httpContextAccessor, IAuthService authService, IOptions<JwtSettingsOptions> options)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
            this._authService = authService;
            this._options = options;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginParameter parameter, [FromServices] IValidator<LoginParameter> validator)
        {
            var logName = $"[{this._httpContextAccessor?.HttpContext?.TraceIdentifier}] [Template_NET_Core] [AuthController] [Login()] [登入]";

            try
            {
                this._logger.LogInformation($"{logName} RQ:{JsonConvert.SerializeObject(logName)}");

                var res = validator.Validate(parameter);
                if (res.IsValid)
                {
                    var loginResult = _authService.Login(new LoginInfoModel() { Username = parameter.Username, Password = parameter.Password });

                    // 假設用戶驗證成功
                    if (loginResult.Result)
                    {
                        // 創建用戶的 Claims，這裡可以自定義 Claims 信息
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, parameter.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        // 密鑰和簽名算法
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key)); // 要與驗證中相同
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        // 創建 JWT Token
                        var token = new JwtSecurityToken(
                            issuer: _options.Value.Issuer,
                            audience: _options.Value.Audience,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(_options.Value.ExpireMinutes),
                            signingCredentials: creds);

                        // 返回 Token
                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                    }

                    return Unauthorized();
                }
                else
                {
                    throw new ValidationException($"{string.Join("、", res.Errors.Select(x => x.ErrorMessage))}");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }
        }
    }

}

