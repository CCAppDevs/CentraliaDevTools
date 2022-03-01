using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class upateViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketStatusId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "StatusTicketStatusId",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    TicketStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.TicketStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_StatusTicketStatusId",
                table: "Ticket",
                column: "StatusTicketStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketStatus_StatusTicketStatusId",
                table: "Ticket",
                column: "StatusTicketStatusId",
                principalTable: "TicketStatus",
                principalColumn: "TicketStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketStatus_StatusTicketStatusId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_StatusTicketStatusId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "StatusTicketStatusId",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "TicketStatusId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
