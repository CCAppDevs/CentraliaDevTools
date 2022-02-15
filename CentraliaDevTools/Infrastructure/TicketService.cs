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
            return _context.Users.FirstOrDefault().Id;
        }

        public string GetUserIDByName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
