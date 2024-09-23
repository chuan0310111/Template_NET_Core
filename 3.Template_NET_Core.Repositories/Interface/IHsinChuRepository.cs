using _3.Template_NET_Core.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Template_NET_Core.Repositories.Interface
{
    public interface IHsinChuRepository
    {
        /// <summary>
        /// 取得鄉鎮市公所名稱
        /// </summary>
        /// <returns></returns>
        Task<List<HsinChuAreaDataModel>> GetAreaAsync();

        /// <summary>
        /// 設定鄉鎮市公所名稱
        /// </summary>
        /// <returns></returns>
        Task<List<HsinChuAreaDataModel>> SetAreaAsync();
    }
}
