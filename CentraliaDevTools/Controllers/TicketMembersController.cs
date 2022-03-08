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

namespace CentraliaDevTools.Controllers
{
    [Authorize]
    public class TicketMembersController : Controller
    {
        private readonly DevToolsContext _context;

        public TicketMembersController(DevToolsContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admins")]
        // GET: TicketMembers
        public async Task<IActionResult> Index()
        {
            var devToolsContext = _context.TicketMembers.Include(t => t.Member).Include(t => t.Ticket);
            return View(await devToolsContext.ToListAsync());
        }
        // GET: TicketMembers/Members/<TicketId>
        public async Task<IActionResult> Members(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var devToolsContext = _context.TicketMembers.Include(t => t.Member).Include(t => t.Ticket).Where(t => t.TicketId == id);

            ViewData["TicketId"] = id;

            return View(await devToolsContext.ToListAsync());
        }

        // GET: TicketMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketMember = await _context.TicketMembers
                .Include(t => t.Member)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.TicketMemberID == id);
            if (ticketMember == null)
            {
                return NotFound();
            }

            return View(ticketMember);
        }

        // GET: TicketMembers/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            return View();
        }

        // New create actions for specific tickets

        // GET: TicketMembers/CreateMemberForTicket/5
        public IActionResult CreateMemberForTicket(int id)
        {
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TicketId"] = id;
            return View();
        }

        // POST: TicketMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketMemberID,TicketId,MemberId")] TicketMember ticketMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Name", ticketMember.MemberId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketMember.TicketId);
            return View(ticketMember);
        }

        // POST: TicketMembers/CreateMemberForTicket/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMemberForTicket(int id, [Bind("TicketMemberID,TicketId,MemberId")] TicketMember ticketMember)
        {
            if (ModelState.IsValid)
            {
                // Set the TicketId to the specific ticket automatically, since there is no field
                ticketMember.TicketId = id;

                _context.Add(ticketMember);
                await _context.SaveChangesAsync();
                return RedirectToAction("Members", "TicketMembers", new { id = id });
            }
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Name", ticketMember.MemberId);
            ViewData["TicketId"] = id;
            return View(ticketMember);
        }

        // GET: TicketMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketMember = await _context.TicketMembers.FindAsync(id);
            if (ticketMember == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName", ticketMember.MemberId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketMember.TicketId);
            return View(ticketMember);
        }

        // POST: TicketMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketMemberID,TicketId,MemberId")] TicketMember ticketMember)
        {
            if (id != ticketMember.TicketMemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketMemberExists(ticketMember.TicketMemberID))
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
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName", ticketMember.MemberId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketMember.TicketId);
            return View(ticketMember);
        }
        [Authorize(Roles = "Admins")]
        // GET: TicketMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketMember = await _context.TicketMembers
                .Include(t => t.Member)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.TicketMemberID == id);
            if (ticketMember == null)
            {
                return NotFound();
            }

            return View(ticketMember);
        }

        // POST: TicketMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketMember = await _context.TicketMembers.FindAsync(id);
            _context.TicketMembers.Remove(ticketMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admins")]
        // GET: TicketMembers/DeleteSpecificTicketMember/5
        public async Task<IActionResult> DeleteSpecificTicketMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketMember = await _context.TicketMembers
                .Include(t => t.Member)
                .Include(t => t.Ticket)
                .FirstOrDefaultAsync(m => m.TicketMemberID == id);
            if (ticketMember == null)
            {
                return NotFound();
            }

            // This is so that we can have the right value for the confirm action
            ViewData["TicketMemberID"] = id;

            return View(ticketMember);
        }

        // POST: TicketMembers/DeleteSpecificTicketMember/5
        [HttpPost, ActionName("DeleteSpecificTicketMember")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSpecificTicketMemberConfirmed(int id)
        {
            var ticketMember = await _context.TicketMembers.FindAsync(id);
            _context.TicketMembers.Remove(ticketMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Members", "TicketMembers", new { id = ticketMember.TicketId });
        }

        private bool TicketMemberExists(int id)
        {
            return _context.TicketMembers.Any(e => e.TicketMemberID == id);
        }
    }
}
