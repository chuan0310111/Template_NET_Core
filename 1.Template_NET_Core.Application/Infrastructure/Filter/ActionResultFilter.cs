using _0.Template_NET_Core.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _1.Template_NET_Core.Application.Infrastructure.Filter
{
    public class ActionResultFilter : IAsyncActionFilter
    {
        private readonly ILogger<ActionResultFilter> _logger;

        public ActionResultFilter(ILogger<ActionResultFilter> logger)
        {
            this._logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {

            this._logger.LogCritical($"In {typeof(ActionResultFilter).FullName}");
            var executeContext = await next();
            if(executeContext.Result is ObjectResult result){
                switch (result.Value)
                {
                    case HttpRequestMessage:
                        return;
                    case SuccessResultViewModel<object>:
                        return;
                    case FailResultViewModel:
                        return;
                }


                var httpMethod = context.HttpContext.Request.Method;
                var responseModel = new SuccessResultViewModel<object>
                {
                    CorrelationId = context.HttpContext.TraceIdentifier,
                    Data = result.Value,
                    ResultCode = "0000",
                    ResultMessage = "Success"
                };

                if(context.HttpContext.Items.TryGetValue("ResultCode", out var code))
                {
                    context.HttpContext.Items.TryGetValue("ResultMessage", out var message);

                    responseModel.ResultCode = code.ToString();
                    responseModel.ResultMessage = message.ToString();

                }

                executeContext.Result = new ObjectResult(responseModel) { 
                    StatusCode = result.StatusCode
                };
            }
        }
    }
}
