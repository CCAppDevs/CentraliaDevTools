using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class memberships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TeamProjects_TeamProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamProjectID",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "TeamProjectMember",
                columns: table => new
                {
                    TeamProjectMemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamProjectId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamProjectMember", x => x.TeamProjectMemberID);
                    table.ForeignKey(
                        name: "FK_TeamProjectMember_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TeamProjectMember_TeamProjects_TeamProjectId",
                        column: x => x.TeamProjectId,
                        principalTable: "TeamProjects",
                        principalColumn: "TeamProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamProjectMember_MemberId",
                table: "TeamProjectMember",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamProjectMember_TeamProjectId",
                table: "TeamProjectMember",
                column: "TeamProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamProjectMember");

            migrationBuilder.AddColumn<int>(
                name: "TeamProjectID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamProjectID",
                table: "AspNetUsers",
                column: "TeamProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TeamProjects_TeamProjectID",
                table: "AspNetUsers",
                column: "TeamProjectID",
                principalTable: "TeamProjects",
                principalColumn: "TeamProjectID");
        }
    }
}
