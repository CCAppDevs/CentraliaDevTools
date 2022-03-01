using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentraliaDevTools.Data;
using CentraliaDevTools.Models;

namespace CentraliaDevTools.Controllers
{
    public class TeamProjectMembersController : Controller
    {
        private readonly DevToolsContext _context;

        public TeamProjectMembersController(DevToolsContext context)
        {
            _context = context;
        }

        // GET: TeamProjectMembers
        public async Task<IActionResult> Index()
        {
            var devToolsContext = _context.Memberships.Include(t => t.Member).Include(t => t.TeamProject);
            return View(await devToolsContext.ToListAsync());
        }

        // GET: TeamProjectMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamProjectMember = await _context.Memberships
                .Include(t => t.Member)
                .Include(t => t.TeamProject)
                .FirstOrDefaultAsync(m => m.TeamProjectMemberID == id);
            if (teamProjectMember == null)
            {
                return NotFound();
            }

            return View(teamProjectMember);
        }

        // GET: TeamProjectMembers/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TeamProjectId"] = new SelectList(_context.TeamProjects, "TeamProjectID", "Name");
            return View();
        }

        // POST: TeamProjectMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamProjectMemberID,TeamProjectId,MemberId,Member")] TeamProjectMember teamProjectMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamProjectMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", teamProjectMember.MemberId);
            ViewData["TeamProjectId"] = new SelectList(_context.TeamProjects, "TeamProjectID", "TeamProjectID", teamProjectMember.TeamProjectId);
            return View(teamProjectMember);
        }

        // GET: TeamProjectMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamProjectMember = await _context.Memberships.FindAsync(id);
            
            if (teamProjectMember == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "UserName", teamProjectMember.MemberId);
            ViewData["TeamProjectId"] = new SelectList(_context.TeamProjects, "TeamProjectID", "Name", teamProjectMember.TeamProjectId);
            return View(teamProjectMember);
        }

        // POST: TeamProjectMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamProjectMemberID,TeamProjectId,MemberId")] TeamProjectMember teamProjectMember)
        {
            if (id != teamProjectMember.TeamProjectMemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamProjectMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamProjectMemberExists(teamProjectMember.TeamProjectMemberID))
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
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Name", teamProjectMember.MemberId);
            ViewData["TeamProjectId"] = new SelectList(_context.TeamProjects, "TeamProjectID", "TeamProjectID", teamProjectMember.TeamProjectId);
            return View(teamProjectMember);
        }

        // GET: TeamProjectMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamProjectMember = await _context.Memberships
                .Include(t => t.Member)
                .Include(t => t.TeamProject)
                .FirstOrDefaultAsync(m => m.TeamProjectMemberID == id);
            if (teamProjectMember == null)
            {
                return NotFound();
            }

            return View(teamProjectMember);
        }

        // POST: TeamProjectMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamProjectMember = await _context.Memberships.FindAsync(id);
            _context.Memberships.Remove(teamProjectMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamProjectMemberExists(int id)
        {
            return _context.Memberships.Any(e => e.TeamProjectMemberID == id);
        }
    }
}
