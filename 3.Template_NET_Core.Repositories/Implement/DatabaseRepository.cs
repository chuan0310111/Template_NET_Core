using _3.Template_NET_Core.Repositories.Conditions;
using _3.Template_NET_Core.Repositories.DataModels;
using _3.Template_NET_Core.Repositories.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace _3.Template_NET_Core.Repositories.Implement
{
    public class DatabaseRepository : IDatabaseRepository
    {

        private readonly ILogger<DatabaseRepository> _logger;
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseRepository(ILogger<DatabaseRepository> logger, IDbConnection dbConnection, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._dbConnection = dbConnection;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 取得使用者登入資訊
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<UsersDataModel> GetUserByUsername(GetUserByUsernameCondition condition)
        {
            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [DatabaseRepository] [GetUserByUsername()] [確認使用者登入資訊]";
            
            this._logger.LogInformation($"{logName} RQ:{JsonConvert.SerializeObject(condition)}");

            throw new HttpRequestException();

            var query = "SELECT * FROM Users WHERE Username = @Username";
            var result = await _dbConnection.QuerySingleOrDefaultAsync<UsersDataModel>(query, new { condition.Username });

            this._logger.LogInformation($"{logName} RP:{JsonConvert.SerializeObject(result)}");

            return result;
        }

    }
}
