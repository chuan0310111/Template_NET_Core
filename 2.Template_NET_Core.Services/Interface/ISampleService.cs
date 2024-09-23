using _2.Template_NET_Core.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Interface
{
    public interface ISampleService
    {

        /// <summary>
        /// 取得鄉鎮市公所名稱 By Cache
        /// </summary>
        /// <returns></returns>
        Task<List<HsinChuAreaDto>> GetAreaAsync();


        /// <summary>
        /// 強制取得鄉鎮市公所名稱 重設cache
        /// </summary>
        /// <returns></returns>
        Task<List<HsinChuAreaDto>> SetAreaAsync();
    }
}
