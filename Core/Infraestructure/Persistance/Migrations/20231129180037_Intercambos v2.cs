using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    public partial class Intercambosv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Alert_ibfk_1",
                table: "Intercambios");

            migrationBuilder.AddForeignKey(
                name: "Intercambio_ibfk_1",
                table: "Intercambios",
                column: "Contrato_Id",
                principalTable: "Contratos",
                principalColumn: "Contrato_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Intercambio_ibfk_1",
                table: "Intercambios");

            migrationBuilder.AddForeignKey(
                name: "Alert_ibfk_1",
                table: "Intercambios",
                column: "Contrato_Id",
                principalTable: "Contratos",
                principalColumn: "Contrato_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
