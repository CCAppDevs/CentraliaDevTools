using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Data;
using CentraliaDevTools.Infrastructure;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CentraliaDevTools.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITicketService _ticketService;
        private readonly DevToolsContext _context;
        private readonly UserManager<DevToolsUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ITicketService ticketService, DevToolsContext context, UserManager<DevToolsUser> userManager)
        {
            _logger = logger;
            _ticketService = ticketService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Filter tickets to just those that the currently logged in user is not a part of (in the TicketMembers list)
            var filteredContext = _context.Ticket.Include(t => t.TicketMembers);

            var newviewModel = new TicketIndexViewModel
            {
                ClosedTickets = _context.Ticket
                   .Include(t => t.TicketMembers)
                   .Include(t => t.TicketStatus)
                   .Where(ticket => ticket.TicketStatusId == 2).ToList(),

                OpenTickets = _context.Ticket
                   .Include(t => t.TicketMembers)
                   .Include(t => t.TicketStatus)
                   .Where(ticket => ticket.TicketStatusId == 1).ToList(),
            };


            return View(newviewModel);
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