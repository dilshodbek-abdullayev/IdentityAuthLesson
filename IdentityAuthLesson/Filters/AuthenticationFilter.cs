using System.Threading.Tasks;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Filters;
using System.Web.Http.Results;


namespace WebApplication1.IdentityFilters
{
    public class AuthenticationFilter
    {
        public class BasicAuthenticationFilter : Attribute, IAuthenticationFilter
        {
            public bool AllowMultiple { get { return false; } }

            public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
            {
                var request = context.Request;
                var authorization = request.Headers.Authorization;

                if (authorization != null && authorization.Scheme == "Basic")
                {
                    string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Parameter));
                    string[] parts = credentials.Split(':');
                    string username = parts[0];
                    string password = parts[1];

                    if (UserAuthenticationService.ValidateUser(username, password))
                    {
                        var identity = new GenericIdentity(username);
                        context.Principal = new GenericPrincipal(identity, null);

                    }
                    else
                    {
                        context.ErrorResult = new UnauthorizedResult("Invalid credentials");
                    }
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult("Authorization header is missing or invalid");
                }
            }

            public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }

    }
}