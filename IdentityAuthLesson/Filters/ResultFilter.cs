using System.Diagnostics;
using System.Web.Http.Filters;

namespace WebApplication1.IdentityFilters
{
    public class ResultFilter
    {
        public class LogResultFilter : ActionFilterAttribute
        {
            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                var response = actionExecutedContext.Response;
                Debug.WriteLine($"Action result: {response.StatusCode}");
            }
        }

    }
}