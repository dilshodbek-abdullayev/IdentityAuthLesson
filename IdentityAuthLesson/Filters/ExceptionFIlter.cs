using System.Diagnostics;
using System.Web.Http.Filters;

namespace WebApplication1.IdentityFilters
{
    public class ExceptionFIlter
    {
        public class LogExceptionFilter : ExceptionFilterAttribute
        {
            public override void OnException(HttpActionExecutedContext actionExecutedContext)
            {
                var exception = actionExecutedContext.Exception;
                Debug.WriteLine($"An exception occurred: {exception.Message}");
            }
        }

    }
}