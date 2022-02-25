using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
    public class TicketMember
    {
        [Key]
        public int TicketMemberID { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public DevToolsUser Member { get; set; }
        public string MemberId { get; set; }


    }
}
