using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    public partial class Intercambosv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Full",
                table: "Instituciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Full",
                table: "Instituciones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
