using _2.Template_NET_Core.Services.Dtos;
using _2.Template_NET_Core.Services.Interface;
using _3.Template_NET_Core.Repositories.Interface;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Implements
{
    public class SampleService : ISampleService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SampleService> _logger;
        private readonly IHsinChuRepository _hsinChuRepository;

        public SampleService(IMapper mapper, ILogger<SampleService> logger, IHsinChuRepository hsinChuRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _hsinChuRepository = hsinChuRepository;
        }

        /// <summary>
        /// 取得鄉鎮市公所名稱 By Cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDto>> GetAreaAsync() {
            var data = await this._hsinChuRepository.GetAreaAsync();
            return this._mapper.Map<List<HsinChuAreaDto>>(data);
        }

        /// <summary>
        /// 強制取得鄉鎮市公所名稱 重設cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<HsinChuAreaDto>> SetAreaAsync()
        {
            var data = await this._hsinChuRepository.SetAreaAsync();
            return this._mapper.Map<List<HsinChuAreaDto>>(data);
        }
    }
}
