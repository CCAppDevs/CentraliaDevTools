using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Data;
using Microsoft.AspNetCore.Identity;

namespace CentraliaDevTools.Models
{
    public class SeedData
    {
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider)
        {
            UserManager<DevToolsUser> userManager = serviceProvider.GetRequiredService<UserManager<DevToolsUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            DevToolsContext context = serviceProvider.GetRequiredService<DevToolsContext>();

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

                List<TicketStatus> statuses = new List<TicketStatus>
                {
                    new TicketStatus {
                        Status = "Open",
                        Priority = 1
                    },
                    new TicketStatus {
                        Status = "Closed",
                        Priority = 1
                    }
                };

                context.TicketStatus.AddRange(statuses);
                await context.SaveChangesAsync();

                List<Ticket> tickets = new List<Ticket>
                {
                    new Ticket {
                        CreatedOn = DateTime.UtcNow,
                        Name = "Test Ticket",
                        TicketStatusId = 0,
                        TicketStatus = statuses[0],
                        Description = "test",
                        Location = "here"
                    },
                    new Ticket {
                        CreatedOn = DateTime.UtcNow,
                        Name = "Test Ticket again",
                        TicketStatusId = 0,
                        TicketStatus = statuses[0],
                        Description = "test",
                        Location = "here"
                    }
                };

                context.Ticket.AddRange(tickets);
                await context.SaveChangesAsync(); // creates the ids

                List<TicketMember> ticketMembers = new List<TicketMember>
                {
                    new TicketMember
                    {
                        Ticket = tickets[0],
                        Member = context.Users.Where(u => u.UserName == username).FirstOrDefault()
                    },
                    new TicketMember
                    {
                        Ticket = tickets[1],
                        Member = context.Users.Where(u => u.UserName == username).FirstOrDefault()
                    }
                };

                context.TicketMembers.AddRange(ticketMembers);
                await context.SaveChangesAsync();
            }
        }
    }
}
