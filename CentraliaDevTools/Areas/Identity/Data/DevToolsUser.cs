using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Identity;

namespace CentraliaDevTools.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DevToolsUser class
public class DevToolsUser : IdentityUser
{
    public List<TeamProject> OwnedProjects { get; set; }
    public List<TeamProjectMember> Memberships { get; set; }
    public List<TicketMember> TicketMembers { get; set; }
    public List<Ticket> AssignedTickets { get; set; }

}

