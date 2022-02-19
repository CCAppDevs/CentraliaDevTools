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
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }
        

    }
}
