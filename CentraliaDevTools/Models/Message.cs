using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
   public class Message
   {
      [Key]
      public int MessageId { get; set; }

      [Required]
      public string MessageText { get; set; }

      [DataType(DataType.Date)]
      public DateTime DateSent { get; set; }

      public bool IsNew { get; set; }

      public string ReceiverId { get; set; }
      public DevToolsUser Sender { get; set; }

      public DevToolsUser Receiver { get; set; }
      
      public List<DevToolsUser> ReceiverList { get; set; }

      public ProjectMessage ProjectMessages { get; set; }

      public TicketMessage TicketMessages { get; set; }
   }

   public class ProjectMessage
   {
      [Key]
      public int ProjectMessageId { get; set; }

      public int MessageId { get; set; }
      public Message Message { get; set; }

      public int TeamProjectId { get; set; }
      public TeamProject TeamProject { get; set; }
   }

   public class TicketMessage 
   {
      [Key]
      public int TicketMessageId { get; set; }

      public int MessageId { get; set; }
      public Message Message { get; set; }

      public int TicketId { get; set; }
      public Ticket Ticket { get; set; }
    }
}
