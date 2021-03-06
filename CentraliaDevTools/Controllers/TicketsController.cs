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
        private readonly DevToolsContext _statusContext;
        private readonly UserManager<DevToolsUser> _userManager;

        public TicketsController(DevToolsContext context, UserManager<DevToolsUser> userManager)
        {
            _context = context;
            _statusContext = context;
            _userManager = userManager;
        }

        // GET: Tickets/Toggle/5

        // Sam Miller

        // Toggles the target ticket's status

        public async Task<IActionResult> Toggle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the ticket
            var ticket = await _context.Ticket.Include(t => t.TicketStatus)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            // Change the ticket's status to the opposite value
            if (ticket.TicketStatusId == 1)  // Ticket is Open, close it
            {
                ticket.TicketStatusId = 2;
                ticket.DateLastClosed = DateTime.Today;
            } else    // Ticket is Closed, open it
            {
                ticket.TicketStatusId = 1;
            }
            // AP - updated Dates updated & closed on toggle
            ticket.DateUpdated = DateTime.Today;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Enroll/5

        // Sam Miller

        // Adds the currently logged in user to the target ticket's members list

        public async Task<IActionResult> Enroll(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get current user
            var user = await _userManager.GetUserAsync(User);

            // Get the ticket
            var ticket = await _context.Ticket.Include(t => t.TicketMembers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            // Create new TicketMember with user and ticket info
            TicketMember tm = new TicketMember
            {
                Ticket = ticket,
                TicketId = ticket.Id,
                Member = user,
                MemberId = user.Id
            };

            // Add the TicketMember to the target ticket
            ticket.TicketMembers.Add(tm);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: Tickets

        // Revision ~ Sam Miller, Feb 20: Limit tickets shown to only those the current user is a member of

        public async Task<IActionResult> Index()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);

            // Filter tickets to just those that the currently logged in user is a part of (in the TicketMembers list)
            var filteredContext = _context.Ticket.Include(t => t.TicketMembers).Where(ticket => ticket.TicketMembers.Any(m => m.MemberId == user.Id));

            var newviewModel = new TicketIndexViewModel
            {
                    ClosedTickets = _context.Ticket
                    .Include(t => t.TicketMembers)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.AssignedUser)
                    .Where(ticket => ticket.TicketMembers.Any(m => m.MemberId == user.Id) && ticket.TicketStatusId == 2).ToList(),

                    OpenTickets = _context.Ticket
                    .Include(t => t.TicketMembers)
                    .Include(t => t.TicketStatus)
                    .Include(t => t.AssignedUser)
                    .Where(ticket => ticket.TicketMembers.Any(m => m.MemberId == user.Id) && ticket.TicketStatusId == 1).ToList(),
            };


            return View(newviewModel);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.Include(t => t.TicketStatus).Include(t => t.AssignedUser)
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
            ViewData["TicketStatusId"] = new SelectList(_statusContext.TicketStatus, "TicketStatusId", "Status");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketID,Name,Location,Description,CreatedOn,DateLastClosed,DateUpdated,TicketStatusId,")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // Add ticket to database
                _context.Add(ticket);
                await _context.SaveChangesAsync();

                // Add a new TicketMember entry with referencing appropriate ticket and member IDs
                var user = await _userManager.GetUserAsync(User);
                TicketMember tm = new();


                tm.TicketId = ticket.Id; // ID of new ticket
                tm.MemberId = user.Id;   // ID of currently logged in user
            

                

                // Add ticketmember to database
                _context.Add(tm);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatus, "TicketStatusId", "Status", ticket.TicketStatusId);
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
            ViewData["TicketStatusId"] = new SelectList(_statusContext.TicketStatus, "TicketStatusId", "Status");
            ViewData["AssignedUserId"] = new SelectList(_statusContext.Users, "Id", "UserName");
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description,CreatedOn, DateLastClosed, DateUpdated, TicketMembers, TicketStatusId, AssignedUserId")] Ticket ticket, TicketMember ticketMember)
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
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatus, "TicketStatusId", "Status", ticket.TicketStatusId);
            ViewData["TicketMembersId"] = new SelectList(_context.TicketMembers, "TicketMemberID", "MemberId", ticketMember.MemberId);
            ViewData["AssignedUserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.AssignedUserId);
            return View(ticket);
            
        }
        [Authorize(Roles = "Admins")]
        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.Include(t => t.TicketStatus).Include(t => t.AssignedUser)
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
