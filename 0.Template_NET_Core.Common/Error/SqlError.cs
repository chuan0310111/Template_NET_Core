using _0.Template_NET_Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0.Template_NET_Core.Common.Error
{
    public class SqlError : IError
    {
        /// <summary>
        /// Sql連線 錯誤訊息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public FailResultViewModel GenerateExceptionMessage(ExceptionContext context)
        {

            var fail = new FailResultViewModel()
            {
                ResultCode = "9030",
                ResultMessage = context.Exception.Message,
                CorrelationId = context.HttpContext.TraceIdentifier
            };

            return fail;
        }
    }
}
