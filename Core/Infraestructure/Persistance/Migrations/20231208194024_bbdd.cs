using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Infraestructure.Persistance.Migrations
{
    public partial class bbdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Institucion_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Ciudad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Pais = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Institucion_Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Rol_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Rol_Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Contrato_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    File = table.Column<string>(type: "longtext", nullable: false),
                    FileAntiguo = table.Column<string>(type: "longtext", nullable: true),
                    Institucion_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Contrato_Id);
                    table.ForeignKey(
                        name: "Agreement_ibfk_1",
                        column: x => x.Institucion_Id,
                        principalTable: "Instituciones",
                        principalColumn: "Institucion_Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AcceptTerms = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Rol_Id = table.Column<int>(type: "int", nullable: true),
                    Institucion_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_Id);
                    table.ForeignKey(
                        name: "User_ibfk_1",
                        column: x => x.Rol_Id,
                        principalTable: "Roles",
                        principalColumn: "Rol_Id");
                    table.ForeignKey(
                        name: "User_institution_FK",
                        column: x => x.Institucion_Id,
                        principalTable: "Instituciones",
                        principalColumn: "Institucion_Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Alerta_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsInstitucion = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Contrato_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Alerta_Id);
                    table.ForeignKey(
                        name: "Alert_ibfk_1",
                        column: x => x.Contrato_Id,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Chat_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Mensaje = table.Column<string>(type: "longtext", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Contrato_Id = table.Column<int>(type: "int", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Chat_Id);
                    table.ForeignKey(
                        name: "Chat_ibfk_1",
                        column: x => x.Contrato_Id,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_Id");
                    table.ForeignKey(
                        name: "Chat_ibfk_2",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Log_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(type: "longtext", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false),
                    Contrato_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Log_Id);
                    table.ForeignKey(
                        name: "Log_ibfk_1",
                        column: x => x.Contrato_Id,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_Id");
                    table.ForeignKey(
                        name: "Log_ibfk_2",
                        column: x => x.Usuario_Id,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_Contrato_Id",
                table: "Alertas",
                column: "Contrato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_Contrato_Id",
                table: "Chats",
                column: "Contrato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_Usuario_Id",
                table: "Chats",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Institucion_Id",
                table: "Contratos",
                column: "Institucion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Nombre",
                table: "Contratos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instituciones_Nombre",
                table: "Instituciones",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intercambios_Contrato_Id",
                table: "Intercambios",
                column: "Contrato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Contrato_Id",
                table: "Logs",
                column: "Contrato_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Usuario_Id",
                table: "Logs",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Institucion_Id",
                table: "Usuarios",
                column: "Institucion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Rol_Id",
                table: "Usuarios",
                column: "Rol_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Intercambios");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Instituciones");
        }
    }
}
