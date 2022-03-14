namespace CentraliaDevTools.Models
{
    public class MessageIndexViewModel
    {
        public List<Message> AllMessages { get; set; }
        public List<Message> FilteredMessages { get; set; }
        public List<Message> TicketMessages { get; set; }
        public List<Message> ProjectMessages { get; set; }
    }
}
