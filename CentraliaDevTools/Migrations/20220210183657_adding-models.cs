using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class addingmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamProjectID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamProjects",
                columns: table => new
                {
                    TeamProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeadId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamProjects", x => x.TeamProjectID);
                    table.ForeignKey(
                        name: "FK_TeamProjects_AspNetUsers_LeadId",
                        column: x => x.LeadId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamProjectID",
                table: "AspNetUsers",
                column: "TeamProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamProjects_LeadId",
                table: "TeamProjects",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TeamProjects_TeamProjectID",
                table: "AspNetUsers",
                column: "TeamProjectID",
                principalTable: "TeamProjects",
                principalColumn: "TeamProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TeamProjects_TeamProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TeamProjects");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamProjectID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamProjectID",
                table: "AspNetUsers");
        }
    }
}
