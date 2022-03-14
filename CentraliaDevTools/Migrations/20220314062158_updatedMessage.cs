using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class updatedMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketMessage_MessageId",
                table: "TicketMessage");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMessage_MessageId",
                table: "ProjectMessage");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_MessageId",
                table: "TicketMessage",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMessage_MessageId",
                table: "ProjectMessage",
                column: "MessageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketMessage_MessageId",
                table: "TicketMessage");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMessage_MessageId",
                table: "ProjectMessage");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_MessageId",
                table: "TicketMessage",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMessage_MessageId",
                table: "ProjectMessage",
                column: "MessageId");
        }
    }
}
