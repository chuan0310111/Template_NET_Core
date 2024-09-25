using _2.Template_NET_Core.Services.Dtos;
using _2.Template_NET_Core.Services.Implements;
using _2.Template_NET_Core.Services.InfoModels;
using _2.Template_NET_Core.Services.Interface;
using _3.Template_NET_Core.Repositories.Conditions;
using _3.Template_NET_Core.Repositories.Interface;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Implement
{
    public class AuthService : IAuthService
    {

        private readonly IMapper _mapper;
        private readonly ILogger<SampleService> _logger;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IMapper mapper, ILogger<SampleService> logger, IDatabaseRepository databaseRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._databaseRepository = databaseRepository;

            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="infoModel"></param>
        /// <returns>true: 登入成功  false:登入失敗</returns>
        public async Task<bool> Login(LoginInfoModel infoModel)
        {
            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [AuthService] [Login()] [登入]";

            this._logger.LogInformation($"{logName} RQ:{JsonConvert.SerializeObject(logName)}");

            try
            {
                var data = await this._databaseRepository.GetUserByUsername(new GetUserByUsernameCondition() { Username = infoModel.Username });

                if (data != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(infoModel.Password, data.PasswordHash)) {

                        this._logger.LogInformation($"{logName} RP:登入成功");
                        return true;
                    }

                    this._logger.LogInformation($"{logName} 密碼輸入錯誤");
                }

                this._logger.LogInformation($"{logName} RP:查無使用者，登入失敗");
                return false;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }

        }
    }
}
