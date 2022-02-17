using CentraliaDevTools.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
    public class TeamProject
    {
        [Key]
        public int TeamProjectID { get; set; }
        public string Name { get; set; }
        public string LeadId { get; set; }
        public DevToolsUser Lead { get; set; }
        public List<TeamProjectMember> Memberships { get; set; }
    }
}
