
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication1.IdentityFilters
{
    public class AuthorizationFilter
    {
        public class AuthorizeRoleAttribute : AuthorizeAttribute
        {
            private readonly string _role;

            public AuthorizeRoleAttribute(string role)
            {
                _role = role;
            }

            protected override bool IsAuthorized(HttpActionContext actionContext)
            {
                var user = actionContext.RequestContext.Principal;
                if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
                {
                    return user.IsInRole(_role);
                }
                return false;
            }
        }

    }
}