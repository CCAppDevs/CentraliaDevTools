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
using CentraliaDevTools.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CentraliaDevTools.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly DevToolsContext _context;
        private readonly UserManager<DevToolsUser> _userManager;

        public MessagesController(DevToolsContext context, UserManager<DevToolsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var messageviewModel = new MessageIndexViewModel
            {
                AllMessages = _context.Message.ToList(),

               

                FilteredMessages = _context.Message
                    .Include(m => m.Sender)
                    .Include(m => m.Receiver)
                    .Where(message => message.Receiver.Id == user.Id || message.Sender.Id == user.Id).ToList(),
               

                TicketMessages = _context.TicketMessage
                    .Include(m => m.TicketMessages)
                    .Where(message => message.Receiver.Id == user.Id || message.Sender.Id == user.Id).ToList()

                //    //ProjectMessages
            };

            return View(messageviewModel);
            //return View();
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public async Task<IActionResult> Create()
        {
            var adminId = _context.Roles.Where(role => role.Name == "Admins").FirstOrDefault().Id.ToString();
            var isAdmin = _context.Users.Where(user => _context.UserRoles.Any(role => role.RoleId == adminId && role.UserId == user.Id)).ToList();
            var user = await _userManager.GetUserAsync(User);

            for (int i = 0; i < isAdmin.Count; i++)
            {
                if (user.Id == isAdmin[i].Id) 
                {
                    //--This pulls all users.I Think this would be good for an admin role. -- \\
                    ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "UserName");

                }
                else
                {
                    ViewData["ReceiverId"] = new SelectList(_context.Users.Where(user => _context.UserRoles.Any(role => role.RoleId == adminId && role.UserId == user.Id)), "Id", "UserName");
                }
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,MessageText,DateSent,IsNew,ReceiverId")] Message message)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                message.Sender = user;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,MessageText,DateSent,IsNew")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.FindAsync(id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }
    }
}
