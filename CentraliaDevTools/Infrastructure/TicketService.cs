using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Data;

namespace CentraliaDevTools.Infrastructure
{
    public class TicketService : ITicketService
    {
        private readonly DevToolsContext _context;

        public TicketService(DevToolsContext ctx)
        {
            _context = ctx;
        }

        public string GetRandomUserID()
        {
            int count = _context.Users.Count();
            int index = new Random().Next(count);

            return _context.Users.Skip(index).FirstOrDefault().Id;
            // return _context.Users.OrderBy(r => Guid.NewGuid()).First().Id;
        }

        public string GetUserIDByName(string userName)
        {
            DevToolsUser user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user.Id;
        }

        public void SendNotification()
        {
            // make an api call to twilio to fire off a notification
            string aUser = GetRandomUserID();
            throw new NotImplementedException();
        }
    }
}
