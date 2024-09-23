using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0.Template_NET_Core.Common.Options
{
    /// <summary>
    /// 新竹縣政府
    /// </summary>
    public class HsinchuGovOptions
    {
        public static readonly string SectionKey = nameof(HsinchuGovOptions);

        /// <summary>
        /// 新竹縣政府Url
        /// </summary>
        public string HsinchuGov_Url { get; set; }

        /// <summary>
        /// 每日幾點cache失效
        /// </summary>
        public int HsinchuGov_CacheExpireAtCertainHour { get; set; }
    }
}