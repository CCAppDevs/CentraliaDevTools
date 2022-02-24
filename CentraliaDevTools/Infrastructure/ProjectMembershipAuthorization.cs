using CentraliaDevTools.Areas.Identity.Data;
using CentraliaDevTools.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CentraliaDevTools.Infrastructure
{
    public class ProjectMembershipAuthorizationRequirement : IAuthorizationRequirement
    {
        // think of this like a model for security information
        public bool AllowMembers { get; set; }
        public bool AllowAdmins { get; set; }
    }

    public class ProjectMembershipAuthorizationHandler : AuthorizationHandler<ProjectMembershipAuthorizationRequirement>
    {
        private UserManager<DevToolsUser> userManager;

        public ProjectMembershipAuthorizationHandler(UserManager<DevToolsUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectMembershipAuthorizationRequirement requirement)
        {
            // determine if the resource is consumable for the given user
            TeamProject project = context.Resource as TeamProject;
            string user = userManager.GetUserId(context.User);

            StringComparison compare = StringComparison.OrdinalIgnoreCase;

            bool isLead = project.LeadId.Equals(user, compare);
            bool isMember = project.Memberships.Exists(m => m.MemberId.Equals(user, compare));

            if (project != null &&
                user != null &&
                (requirement.AllowMembers && (isLead || isMember)) ||
                (requirement.AllowAdmins && context.User.IsInRole("Admins"))
            ) {
                context.Succeed(requirement);
            } else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
