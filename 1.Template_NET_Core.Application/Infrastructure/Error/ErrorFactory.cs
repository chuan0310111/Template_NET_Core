using _0.Template_NET_Core.Common.Error;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _1.Template_NET_Core.Application.Infrastructure.Error
{
    /// <summary>
    /// Class ExceptionHelper
    /// </summary>
    public class ErrorFactory
    {

        private static readonly Dictionary<string, IError> ExceptionCategory;

        static ErrorFactory()
        {
            ExceptionCategory = new Dictionary<string, IError>()
            {
                { "SqlException", new SqlError() },
                { "HttpRequestException", new _0.Template_NET_Core.Common.Error.HttpRequestError() },
                { "ValidationException" , new ValidationError() }
            };
        }

        /// <summary>
        /// 取得例外類型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IError GetExceptionType(ExceptionContext context)
        {
            var exceptionType = ExceptionCategory.Where(
                x => x.Key == context.Exception.GetType()?.Name ||
                        x.Key == context.Exception.InnerException?.GetType().Name)
                        .Select(x => x.Value)
                        .FirstOrDefault();

            return exceptionType;
        }
    }
}
