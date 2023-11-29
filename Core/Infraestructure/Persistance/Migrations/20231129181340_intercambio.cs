using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    public partial class intercambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "Intercambio_ibfk_1",
                        column: x => x.Contrato_Id,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_Id");
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
        }
    }
}
