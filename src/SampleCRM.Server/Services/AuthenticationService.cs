using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using OpenRiaServices.Server;
using OpenRiaServices.Server.Authentication;

namespace SampleCRM.Web
{
    // TODO: Switch to a secure endpoint when deploying the application.
    //       The user's name and password should only be passed using https.
    //       To do this, set the RequiresSecureEndpoint property on EnableClientAccessAttribute to true.
    //
    //       [EnableClientAccess(RequiresSecureEndpoint = true)]
    //
    //       More information on using https with a Domain Service can be found on MSDN.

    [EnableClientAccess]
    public class AuthenticationService : DomainService, IAuthentication<User>
    {
        protected HttpContext HttpContext
          => base.ServiceContext.GetRequiredService<IHttpContextAccessor>().HttpContext!;

        [AllowAnonymous]
        public User GetUser()
        {
            var user = (ClaimsPrincipal)base.ServiceContext.User;

            return (user.Identity?.IsAuthenticated == true)
                ? new User() { Name = user.Identity.Name!, Roles = GetRoles(user) }
                : new User() { Name = string.Empty, Roles = Array.Empty<string>() };
        }

        private static string[] GetRoles(ClaimsPrincipal principal)
        {
            return principal.FindAll(ClaimTypes.Role)
                .Select(claim => claim.Value)
                .ToArray();
        }

        [AllowAnonymous]
        public User Login(string userName, string password, bool isPersistent, string customData)
        {

            // TODO: Ensure username/passwords (and custom data) are validated
            if (userName is "manager" or "employee")
            {
                // TODO: Maybe the claims is better extraced/created based on the "User" type
                // - ensure corrects roles are given
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties()
                    {
                        IsPersistent = isPersistent
                    })
                    .GetAwaiter().GetResult();

                return new User()
                {
                    Name = userName,
                    Roles = new[] { userName }
                };
            }
            else
            {
                // login failure
                return null;
            }
        }

        public User Logout()
        {
            HttpContext.SignOutAsync()
                .GetAwaiter().GetResult();

            return new User() { Name = string.Empty, Roles = Array.Empty<string>() };
        }

        public void UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}