using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CentraliaDevTools.Data;
using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Infrastructure;
using CentraliaDevTools.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DevToolsContextConnection");

builder.Services.AddDbContext<DevToolsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DevToolsUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DevToolsContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<DevToolsUser>, DevToolsPrincipalFactory>();
builder.Services.AddScoped<ITicketService, TicketService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
   await SeedData.CreateAdminAccount(services);
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
