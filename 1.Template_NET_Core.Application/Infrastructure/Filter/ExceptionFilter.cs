using _1.Template_NET_Core.Application.Infrastructure.Error;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using _0.Template_NET_Core.Common.ViewModels;
using System;

namespace _1.Template_NET_Core.Application.Infrastructure.Filter
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger) {
            this._logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            this._logger?.LogError(context.Exception.ToString());
            var error = ErrorFactory.GetExceptionType(context);

            if (error != null)
            {
                var exception = error.GenerateExceptionMessage(context);
                context.Result = new ObjectResult(exception)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                if (context.Exception.Data.Contains("ResultCode"))
                {
                    var returnCode = context.Exception.Data["ResultCode"];
                    exception.ResultCode = returnCode.ToString();
                }

                if (context.Exception.Data.Contains("ResultMessage"))
                {
                    var returnMessage = context.Exception.Data["ResultMessage"];
                    exception.ResultMessage = returnMessage.ToString();
                }

                context.ExceptionHandled = true;
                await Task.Yield();
            }
            else {

                var result = new FailResultViewModel
                {
                    CorrelationId = context.HttpContext.TraceIdentifier,
                    ResultCode = "9999",
                    ResultMessage = "系統發生未預期的錯誤，請稍後再試(9999)"
                };

                if (context.Exception.Data.Contains("ResultCode"))
                {
                    var returnCode = context.Exception.Data["ResultCode"];
                    result.ResultCode = returnCode.ToString();
                }

                if (context.Exception.Data.Contains("ResultMessage"))
                {
                    var returnMessage = context.Exception.Data["ResultMessage"];
                    result.ResultMessage = returnMessage.ToString();
                }

                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                context.ExceptionHandled = true;
                await Task.Yield();

            }
        }
    }
}
