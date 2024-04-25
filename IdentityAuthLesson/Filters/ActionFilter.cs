using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication1.IdentityFilters
{
    public class ActionFilter
    {
        public class LogActionFilter : System.Web.Http.Filters.ActionFilterAttribute
        {
            public override void OnActionExecuting(HttpActionContext actionContext)
            {
                Debug.WriteLine($"Action '{actionContext.ActionDescriptor.ActionName}' is starting...");
            }

            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
                Debug.WriteLine($"Action '{actionExecutedContext.ActionContext.ActionDescriptor.ActionName}' has completed.");
            }
        }

    }
}