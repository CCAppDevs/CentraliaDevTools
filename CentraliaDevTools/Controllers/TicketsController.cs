using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentraliaDevTools.Data;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CentraliaDevTools.Areas.Identity.Data;

namespace CentraliaDevTools.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly DevToolsContext _context;
        private readonly UserManager<DevToolsUser> _userManager;

        public TicketsController(DevToolsContext context, UserManager<DevToolsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets

        // Revision ~ Sam Miller, Feb 20: Limit tickets shown to only those the current user is a member of

        public async Task<IActionResult> Index()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);

            // Filter tickets to just those that the currently logged in user is a part of (in the TicketMembers list)
            var filteredContext = _context.Ticket.Include(t => t.TicketMembers).Where(ticket => ticket.TicketMembers.Any(m => m.MemberId == user.Id));

            // AP 2/27 Added Status to context   
            //filteredContext.Include(ticket => ticket.TicketStatusId);

            // Pass filtered data to the view
            return View(await filteredContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Name,Location,Description,CreatedOn")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // Add ticket to database
                _context.Add(ticket);
                await _context.SaveChangesAsync();

                // Add a new TicketMember entry with referencing appropriate ticket and member IDs
                var user = await _userManager.GetUserAsync(User);
                TicketMember tm = new TicketMember();

            // AP
            // int TicketStatusID = ticket.TicketStatusId;


               tm.TicketId = ticket.Id; // ID of new ticket
                tm.MemberId = user.Id;   // ID of currently logged in user
            

                

                // Add ticketmember to database
                _context.Add(tm);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description,CreatedOn")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
