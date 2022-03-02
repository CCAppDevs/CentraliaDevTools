using CentraliaDevTools.Infrastructure;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CentraliaDevTools.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CentraliaDevTools.Areas.Identity.Data;

namespace CentraliaDevTools.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketService _ticketService;

        private readonly DevToolsContext _context;
        private readonly UserManager<DevToolsUser> _userManager;

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            // Filter tickets to just those that the currently logged in user is not a part of (in the TicketMembers list)
            var filteredContext = _context.Ticket.Include(t => t.TicketMembers).Include(t => t.TicketStatus).Where(ticket => ticket.TicketMembers.Any(m => m.MemberId != user.Id));

            // Pass filtered data to the view
            return View(await filteredContext.ToListAsync());
        }
        public HomeController(ILogger<HomeController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {

            ViewData["RandomID"] = _ticketService.GetRandomUserID();
            ViewData["JesseID"] = _ticketService.GetUserIDByName("jesse@jesse.com");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}