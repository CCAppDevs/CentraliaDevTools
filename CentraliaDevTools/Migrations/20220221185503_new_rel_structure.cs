using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class new_rel_structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ticket",
                newName: "TicketID");

            migrationBuilder.AddColumn<string>(
                name: "DevToolsUserId",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DevToolsUserId",
                table: "Ticket",
                column: "DevToolsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_DevToolsUserId",
                table: "Ticket",
                column: "DevToolsUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_DevToolsUserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_DevToolsUserId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "DevToolsUserId",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "TicketID",
                table: "Ticket",
                newName: "Id");
        }
    }
}
