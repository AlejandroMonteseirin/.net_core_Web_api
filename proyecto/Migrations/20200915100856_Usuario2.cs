using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace proyecto.Migrations
{
    public partial class Usuario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    nace = table.Column<DateTime>(nullable: true),
                    mensaje = table.Column<string>(nullable: true),
                    apellidos = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: false),
                    email = table.Column<string>(nullable: true),
                    FechaCreado = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
