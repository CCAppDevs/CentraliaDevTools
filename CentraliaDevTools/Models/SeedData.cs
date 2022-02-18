using CentraliaDevTools.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CentraliaDevTools.Models
{
    public class SeedData
    {
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider)
        {
            UserManager<DevToolsUser> userManager = serviceProvider.GetRequiredService<UserManager<DevToolsUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin@site.com";
            string email = "admin@site.com";
            string password = "123Secret!"; // must follow password guidelines
            string role = "Admins";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                DevToolsUser user = new DevToolsUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
