using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class addedNavprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedUserId",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedUserId",
                table: "Ticket",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_AspNetUsers_AssignedUserId",
                table: "Ticket",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_AspNetUsers_AssignedUserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_AssignedUserId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Ticket");
        }
    }
}
