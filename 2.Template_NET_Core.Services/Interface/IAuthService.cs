using _2.Template_NET_Core.Services.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Template_NET_Core.Services.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="infoModel"></param>
        /// <returns>true: 登入成功  false:登入失敗</returns>
        Task<bool> Login(LoginInfoModel infoModel);
    }
}
