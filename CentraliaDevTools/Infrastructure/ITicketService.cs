namespace CentraliaDevTools.Infrastructure
{
    public interface ITicketService
    {
        string GetRandomUserID();
        string GetUserIDByName(string userName);
    }
}
