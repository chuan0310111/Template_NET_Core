using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Template_NET_Core.Repositories.DataModels
{
    /// <summary>
    /// User資料表
    /// </summary>
    public class UsersDataModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
