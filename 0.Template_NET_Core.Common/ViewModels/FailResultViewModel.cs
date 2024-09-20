using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0.Template_NET_Core.Common.ViewModels
{
    /// <summary>
    /// FailResultViewModel
    /// </summary>
    public class FailResultViewModel
    {
        /// <summary>
        /// 執行結果代碼
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// 執行結果說明
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// Request識別碼
        /// </summary>
        public string CorrelationId {  get; set; }
    }
}
