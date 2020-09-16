using Microsoft.EntityFrameworkCore.Migrations;

namespace proyecto.Migrations
{
    public partial class Apellidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "apellidos",
                table: "Contactos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apellidos",
                table: "Contactos");
        }
    }
}
