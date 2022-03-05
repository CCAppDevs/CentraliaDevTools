using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
   public class Message
   {
      [Key]
      public int MessageId { get; set; }
      public string MessageText { get; set; }
      public DevToolsUser Sender { get; set; }
      public DevToolsUser Receiver { get; set; }
      public List<DevToolsUser> ReceiverList { get; set; }
   }

   public class ProjectMessage
   {
      [Key]
      public Message MessageId { get; set; }

      int ProjectId { get; set; }
   }

   public class TicketMessage 
   {
      [Key]
      public Message MessageId { get; set; }
      int TicketMessageId { get; set; }
   }
}
