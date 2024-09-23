using _0.Template_NET_Core.Common.Options;
using _3.Template_NET_Core.Repositories.DataModels;
using _3.Template_NET_Core.Repositories.Implement;
using _3.Template_NET_Core.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _3.Template_NET_Core.Repositories.Cached
{
    public class CachedHsinChuRepository : IHsinChuRepository
    {
        private readonly ILogger<HsinChuRepository> _logger;
        private readonly IOptions<HsinchuGovOptions> _options;
        private readonly IMemoryCache _cache;
        private readonly IHsinChuRepository _hsinChuRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cacheKey = "HsinchuGovKey";

        private MemoryCacheEntryOptions cacheOptions;

        public CachedHsinChuRepository(ILogger<HsinChuRepository> logger, IOptions<HsinchuGovOptions> options, IMemoryCache cache, IHsinChuRepository hsinChuRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._options = options;
            this._cache = cache;
            this._hsinChuRepository = hsinChuRepository;

            // 當地的本地時間，並包含與UTC的時間差(偏移量) ex.2024-09-23 15:30:00 +08:00
            var nowDateTime = DateTimeOffset.Now;

            // 取得每日幾點
            var setDateTime = new DateTimeOffset(new DateTime(nowDateTime.Year, nowDateTime.Month, nowDateTime.Day, _options.Value.HsinchuGov_CacheExpireAtCertainHour, 0, 0));
            if (setDateTime < nowDateTime)
            {
                setDateTime = setDateTime.AddDays(1);
            }

            // 設定cache失效時間
            this.cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(absolute: setDateTime);

            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 取得鄉鎮市公所名稱 By Cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDataModel>> GetAreaAsync() {

            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [CachedHsinChuRepository] [GetAreasAsync()] [取得鄉鎮市公所名稱 By Cache]";
            try
            {
                this._logger.LogInformation($"{logName} 鄉鎮市公所Cache查詢 RQ");
                var data = await this._cache.GetOrCreateAsync(
                    _cacheKey,
                    async entry =>
                    {
                        this._logger.LogInformation($"{logName} 鄉鎮市公所API查詢 RQ");

                        entry.SetOptions(cacheOptions);
                        var result = await _hsinChuRepository.GetAreaAsync();
                        this._logger.LogInformation($"{logName} 鄉鎮市公所API查詢 RP By API count{result.Count} cache時間:{cacheOptions.AbsoluteExpiration}");

                        return result;

                    });

                this._logger.LogInformation($"{logName} 鄉鎮市公所API查詢 RP By Cache count{data.Count}");
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }
        }

        /// <summary>
        /// 強制取得鄉鎮市公所名稱 重設cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDataModel>> SetAreaAsync() {

            var logName = $"[{this._httpContextAccessor.HttpContext.TraceIdentifier}] [Template_NET_Core] [CachedHsinChuRepository] [SetAreaAsync()] [強制取得鄉鎮市公所名稱 重設cache]";
            try
            {
                this._logger.LogInformation($"{logName} 鄉鎮市公所API查詢 RQ");

                var result = await _hsinChuRepository.GetAreaAsync();
                _cache.Set(_cacheKey, result, cacheOptions);

                this._logger.LogInformation($"{logName} 鄉鎮市公所API查詢 RP By API count{result.Count} cache時間:{cacheOptions.AbsoluteExpiration}");
                return result;

            }
            catch (Exception ex)
            {
                this._logger.LogError($"{logName} ex: {ex.Message}");
                throw ex;
            }
        }

    }
}
