using System.ComponentModel.DataAnnotations;
namespace CentraliaDevTools.Models
{
    public class Ticket
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<TicketMember> TicketMembers { get; set; }
        
        public int TicketStatusId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateLastClosed { get; set; }
        //Made this a nullable field that way we only import a date once edited. - Jordan M
        [DataType(DataType.DateTime)]
        public DateTime? DateUpdated { get; set; }

    }
}
