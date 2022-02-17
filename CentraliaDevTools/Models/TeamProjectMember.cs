using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
    public class TeamProjectMember
    {
        [Key]
        public int TeamProjectMemberID { get; set; }
        public int TeamProjectId { get; set; }
        public TeamProject TeamProject { get; set; }
        public string MemberId { get; set; }
        public DevToolsUser Member { get; set; }
    }
}
