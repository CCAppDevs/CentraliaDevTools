using CentraliaDevTools.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace TodoApp.Infrastructure
{
    public class DevToolsPrincipalFactory : UserClaimsPrincipalFactory<DevToolsUser, IdentityRole>
    {
        public DevToolsPrincipalFactory(
            UserManager<DevToolsUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(DevToolsUser user)
        {
            // gather any services and data we need
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();

            // validate it
            if (true)
            {
                claims.Add(new Claim("CustomClaim", "some thing"));
            }
            else
            {
                claims.Add(new Claim("CustomClaim", "some other thing"));
            }

            // add our claims

            identity.AddClaims(claims);

            return principal;
        }
    }
}