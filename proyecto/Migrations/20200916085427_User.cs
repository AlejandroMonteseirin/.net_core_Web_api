using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace proyecto.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    FechaCreado = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.username);
                });
        }
    }
}
