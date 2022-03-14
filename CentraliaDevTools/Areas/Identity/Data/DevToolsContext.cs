﻿using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CentraliaDevTools.Data;

public class DevToolsContext : IdentityDbContext<DevToolsUser>
{
    public DevToolsContext(DbContextOptions<DevToolsContext> options)
        : base(options)
    {
    }

    public DbSet<TeamProject> TeamProjects { get; set; }
    public DbSet<TeamProjectMember> Memberships { get; set; }
    public DbSet<TicketMember> TicketMembers { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<TicketStatus> TicketStatus { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }


    public DbSet<CentraliaDevTools.Models.Message> Message { get; set; }
    public DbSet<CentraliaDevTools.Models.Message> TicketMessage { get; set; }


}
