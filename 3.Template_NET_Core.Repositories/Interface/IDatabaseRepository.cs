using _3.Template_NET_Core.Repositories.Conditions;
using _3.Template_NET_Core.Repositories.DataModels;
using _3.Template_NET_Core.Repositories.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Template_NET_Core.Repositories.Interface
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// 取得使用者登入資訊
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<UsersDataModel> GetUserByUsername(GetUserByUsernameCondition condition);
    }
}
