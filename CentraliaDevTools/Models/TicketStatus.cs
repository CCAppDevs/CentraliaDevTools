using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
   public class TicketStatus
   {
      [Key]
      public int TicketStatusId { get; set; }
      public string Status { get; set; }
      public int Priority { get; set; }
   }
}
