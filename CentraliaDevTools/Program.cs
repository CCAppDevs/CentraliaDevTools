using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CentraliaDevTools.Data;
using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Infrastructure;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DevToolsContextConnection");

builder.Services.AddDbContext<DevToolsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DevToolsUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DevToolsContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<DevToolsUser>, DevToolsPrincipalFactory>();
builder.Services.AddScoped<ITicketService, TicketService>();

// create the policy instance
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ProjectMembersOrAdmins", policy =>
//    {
//        policy.AddRequirements(new ProjectMembershipAuthorizationRequirement
//        {
//            AllowAdmins = true,
//            AllowMembers = true
//        });
//    });
//});

// inject the policy handler
//builder.Services.AddTransient<IAuthorizationHandler, ProjectMembershipAuthorizationHandler>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Run Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.CreateAdminAccount(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
