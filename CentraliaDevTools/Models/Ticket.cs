using System.ComponentModel.DataAnnotations;
using CentraliaDevTools.Areas.Identity.Data;

namespace CentraliaDevTools.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<TicketMember> TicketMembers { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        public List<DevToolsUser> TicketUsers;
    }
}
