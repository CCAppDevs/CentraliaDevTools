using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class updatingMessageModelForEasierFiltering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMessage");

            migrationBuilder.DropTable(
                name: "TicketMessage");

            migrationBuilder.AddColumn<int>(
                name: "TeamProjectId",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_TeamProjectId",
                table: "Message",
                column: "TeamProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_TicketId",
                table: "Message",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_TeamProjects_TeamProjectId",
                table: "Message",
                column: "TeamProjectId",
                principalTable: "TeamProjects",
                principalColumn: "TeamProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Ticket_TicketId",
                table: "Message",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_TeamProjects_TeamProjectId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Ticket_TicketId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_TeamProjectId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_TicketId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "TeamProjectId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Message");

            migrationBuilder.CreateTable(
                name: "ProjectMessage",
                columns: table => new
                {
                    ProjectMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    TeamProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMessage", x => x.ProjectMessageId);
                    table.ForeignKey(
                        name: "FK_ProjectMessage_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMessage_TeamProjects_TeamProjectId",
                        column: x => x.TeamProjectId,
                        principalTable: "TeamProjects",
                        principalColumn: "TeamProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessage",
                columns: table => new
                {
                    TicketMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessage", x => x.TicketMessageId);
                    table.ForeignKey(
                        name: "FK_TicketMessage_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketMessage_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMessage_MessageId",
                table: "ProjectMessage",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMessage_TeamProjectId",
                table: "ProjectMessage",
                column: "TeamProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_MessageId",
                table: "TicketMessage",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessage_TicketId",
                table: "TicketMessage",
                column: "TicketId");
        }
    }
}
