using _0.Template_NET_Core.Common.Options;
using _3.Template_NET_Core.Repositories.DataModels;
using _3.Template_NET_Core.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace _3.Template_NET_Core.Repositories.Implement
{
    public class HsinChuRepository : IHsinChuRepository
    {
        private readonly ILogger<HsinChuRepository> _logger;
        private readonly IOptions<HsinchuGovOptions> _options;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HsinChuRepository(ILogger<HsinChuRepository> logger, IOptions<HsinchuGovOptions> options, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._options = options;
            this._httpClientFactory = httpClientFactory;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 取得鄉鎮市公所名稱
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDataModel>> GetAreaAsync() {

            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [HsinChuRepository] [GetAreaAsync()] [取得鄉鎮市公所名稱]";

            this._logger.LogInformation($"{logName} RQ");

            return await SetAreaAsync();
        }


        /// <summary>
        /// 取得鄉鎮市公所名稱(By API)
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDataModel>> SetAreaAsync() {

            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [HsinChuRepository] [SetAreaAsync()] [設定鄉鎮市公所名稱]";

            this._logger.LogInformation($"{logName} RQ");

            try
            {
                var client = this._httpClientFactory.CreateClient("HsinchuGovOptions");
                var response = await client.GetAsync(_options.Value.HsinchuGov_Url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JArray.Parse(content).ToObject<IEnumerable<HsinChuAreaDataModel>>();
                if (result?.ToList().Count > 0) {

                    this._logger.LogInformation($"{logName} Rp Data: {content}");

                    return result.ToList();

                }
                else{
                    throw new HttpRequestException($"{logName} API回應格式異常: Rp 資料為空 [{content}]");
                }
            }
            catch (Exception ex)
            {

                throw new HttpRequestException($"{logName} API回應格式異常: {_options.Value.HsinchuGov_Url} 發生異常 ex:{ex.ToString()}");
            }
        }

    }
}
