using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    public partial class Intercambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Full",
                table: "Instituciones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Fase",
                table: "Contratos",
                type: "longtext",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Intercambios",
                columns: table => new
                {
                    Intercambio_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Revision = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Firma = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Contrato_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intercambios", x => x.Intercambio_Id);
                    table.ForeignKey(
                        name: "Alert_ibfk_1",
                        column: x => x.Contrato_Id,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_Contrato_Id",
                table: "Intercambios",
                column: "Contrato_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intercambios");

            migrationBuilder.DropColumn(
                name: "Full",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "Fase",
                table: "Contratos");
        }
    }
}
