using _0.Template_NET_Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0.Template_NET_Core.Common.Error
{
    public interface IError
    {
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        FailResultViewModel GenerateExceptionMessage(ExceptionContext context);
    }
}
