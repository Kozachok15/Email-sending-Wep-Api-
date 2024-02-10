using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailsWepApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Body = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Recipients = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Result = table.Column<bool>(type: "bit", nullable: false),
                    FailedMessage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mail", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mail");
        }
    }
}
