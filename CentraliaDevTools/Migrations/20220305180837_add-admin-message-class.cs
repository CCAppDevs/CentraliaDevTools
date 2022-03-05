using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentraliaDevTools.Migrations
{
    public partial class addadminmessageclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminMessage",
                columns: table => new
                {
                    AdminMessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMessage", x => x.AdminMessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMessage");
        }
    }
}
