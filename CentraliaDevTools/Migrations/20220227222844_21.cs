using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketStatus_StatusId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_StatusId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "TicketStatusId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketStatusId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_StatusId",
                table: "Ticket",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketStatus_StatusId",
                table: "Ticket",
                column: "StatusId",
                principalTable: "TicketStatus",
                principalColumn: "Id");
        }
    }
}
