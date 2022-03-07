using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
namespace CentraliaDevTools.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        public List<TicketMember> TicketMembers { get; set; }
        public string AssignedUserId { get; set; }
        public DevToolsUser AssignedUser { get; set; }
        public int TicketStatusId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateLastClosed { get; set; }
        //Made this a nullable field that way we only import a date once edited. - Jordan M
        [DataType(DataType.Date)]
        public DateTime? DateUpdated { get; set; }
        

    }
}
