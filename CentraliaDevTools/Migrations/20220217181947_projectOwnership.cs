using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class projectOwnership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamProjectMember_AspNetUsers_MemberId",
                table: "TeamProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamProjectMember_TeamProjects_TeamProjectId",
                table: "TeamProjectMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamProjectMember",
                table: "TeamProjectMember");

            migrationBuilder.RenameTable(
                name: "TeamProjectMember",
                newName: "Memberships");

            migrationBuilder.RenameIndex(
                name: "IX_TeamProjectMember_TeamProjectId",
                table: "Memberships",
                newName: "IX_Memberships_TeamProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamProjectMember_MemberId",
                table: "Memberships",
                newName: "IX_Memberships_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "TeamProjectMemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_AspNetUsers_MemberId",
                table: "Memberships",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_TeamProjects_TeamProjectId",
                table: "Memberships",
                column: "TeamProjectId",
                principalTable: "TeamProjects",
                principalColumn: "TeamProjectID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_AspNetUsers_MemberId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_TeamProjects_TeamProjectId",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "TeamProjectMember");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_TeamProjectId",
                table: "TeamProjectMember",
                newName: "IX_TeamProjectMember_TeamProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_MemberId",
                table: "TeamProjectMember",
                newName: "IX_TeamProjectMember_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamProjectMember",
                table: "TeamProjectMember",
                column: "TeamProjectMemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamProjectMember_AspNetUsers_MemberId",
                table: "TeamProjectMember",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamProjectMember_TeamProjects_TeamProjectId",
                table: "TeamProjectMember",
                column: "TeamProjectId",
                principalTable: "TeamProjects",
                principalColumn: "TeamProjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
