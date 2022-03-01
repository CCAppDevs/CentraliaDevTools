﻿  
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
   public class TeamProjectsController : Controller
   {
	  private readonly DevToolsContext _context;
	  private readonly UserManager<DevToolsUser> _userManager;
	  private readonly IAuthorizationService _auth;

	  public TeamProjectsController(DevToolsContext context, UserManager<DevToolsUser> usrMgr, IAuthorizationService auth)
	  {
		 _context = context;
		 _userManager = usrMgr;
		 _auth = auth;
	  }

	  // GET: TeamProjects
	  public async Task<IActionResult> Index()
	  {
		 var user = await _userManager.GetUserAsync(User);
		 var devToolsContext = _context.TeamProjects.Include(t => t.Lead).Where(p => p.LeadId == user.Id);
		 return View(await devToolsContext.ToListAsync());
	  }

	  // GET: TeamProjects/Details/5
	  public async Task<IActionResult> Details(int? id)
	  {
		 if (id == null)
		 {
			return NotFound();
		 }

		 var teamProject = await _context.TeamProjects
			 .Include(t => t.Lead)
			 .FirstOrDefaultAsync(m => m.TeamProjectID == id);
		 if (teamProject == null)
		 {
			return NotFound();
		 }

		 return View(teamProject);
	  }

	  // GET: TeamProjects/Create
	  public async Task<IActionResult> Create()
	  {
		 var user = await _userManager.GetUserAsync(User);
		 ViewData["LeadId"] = user.Id;
		 return View();
	  }

	  // POST: TeamProjects/Create
	  // To protect from overposting attacks, enable the specific properties you want to bind to.
	  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	  [HttpPost]
	  [ValidateAntiForgeryToken]
	  public async Task<IActionResult> Create([Bind("TeamProjectID,Name,LeadId")] TeamProject teamProject)
	  {
		 if (ModelState.IsValid)
		 {
			_context.Add(teamProject);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		 }
		 ViewData["LeadId"] = new SelectList(_context.Users, "Id", "UserName", teamProject.LeadId);
		 return View(teamProject);
	  }

	  // GET: TeamProjects/Edit/5
	  public async Task<IActionResult> Edit(int? id)
	  {
			if (id == null)
			{
				return NotFound();
			}

			var teamProject = _context.TeamProjects.Include(t => t.Memberships).Where(t => t.TeamProjectID == id).FirstOrDefault();

			if (teamProject == null)
			{
				return NotFound();
			}

			AuthorizationResult authorized = await _auth.AuthorizeAsync(User, teamProject, "MembersAndAdmins");

			if (authorized.Succeeded)
			{
				ViewData["LeadId"] = new SelectList(_context.Users, "Id", "UserName", teamProject.LeadId);
				return View(teamProject);
			}
			else
			{
				return new ChallengeResult();
			}
	  }

	  // POST: TeamProjects/Edit/5
	  // To protect from overposting attacks, enable the specific properties you want to bind to.
	  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	  [HttpPost]
	  [ValidateAntiForgeryToken]
	  public async Task<IActionResult> Edit(int id, [Bind("TeamProjectID,Name,LeadId")] TeamProject teamProject)
	  {
		 if (id != teamProject.TeamProjectID)
		 {
			return NotFound();
		 }

		 if (ModelState.IsValid)
		 {
			try
			{
			   _context.Update(teamProject);
			   await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
			   if (!TeamProjectExists(teamProject.TeamProjectID))
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
		 ViewData["LeadId"] = new SelectList(_context.Users, "Id", "UserName", teamProject.LeadId);
		 return View(teamProject);
	  }

	  // GET: TeamProjects/Delete/5
	  public async Task<IActionResult> Delete(int? id)
	  {
		 if (id == null)
		 {
			return NotFound();
		 }

		 var teamProject = await _context.TeamProjects
			 .Include(t => t.Lead)
			 .Include(t => t.Memberships)
			 .FirstOrDefaultAsync(m => m.TeamProjectID == id);
		 if (teamProject == null)
		 {
			return NotFound();
		 }

		AuthorizationResult authorized = await _auth.AuthorizeAsync(User, teamProject, "MembersAndAdmins");

		if (authorized.Succeeded)
		{
			return View(teamProject);
		}
		else
		{
			return new ChallengeResult();
		}
	  }

	  // POST: TeamProjects/Delete/5
	  [HttpPost, ActionName("Delete")]
	  [ValidateAntiForgeryToken]
	  public async Task<IActionResult> DeleteConfirmed(int id)
	  {
		 var teamProject = await _context.TeamProjects.FindAsync(id);
		 _context.TeamProjects.Remove(teamProject);
		 await _context.SaveChangesAsync();
		 return RedirectToAction(nameof(Index));
	  }

	  private bool TeamProjectExists(int id)
	  {
		 return _context.TeamProjects.Any(e => e.TeamProjectID == id);
	  }
   }
}