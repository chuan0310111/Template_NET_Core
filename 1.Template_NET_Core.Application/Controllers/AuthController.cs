using _1.Template_NET_Core.Application.Parameters;
using _1.Template_NET_Core.Application.ViewModels;
using _2.Template_NET_Core.Services.Implement;
using _2.Template_NET_Core.Services.InfoModels;
using _2.Template_NET_Core.Services.Interface;
using _3.Template_NET_Core.Repositories.Conditions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IMapper mapper, ILogger<SampleController> logger, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
            this._authService = authService;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginParameter parameter)
        {
            var logName = $"[{this._httpContextAccessor?.HttpContext?.TraceIdentifier}] [Template_NET_Core] [AuthController] [Login()] [登入]";

            try
            {
                this._logger.LogInformation($"{logName} RQ:{JsonConvert.SerializeObject(logName)}");

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
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vN8g4LjNWyVbmGRRBgIXgvGlV/nRIvYIshZw7H3vqZI=")); // 要與驗證中相同
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // 創建 JWT Token
                    var token = new JwtSecurityToken(
                        issuer: "https://mia1-issuer.com",
                        audience: "miaAudience",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    // 返回 Token
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }
        }
    }

}

