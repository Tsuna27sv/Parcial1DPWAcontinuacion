using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial1DPWA.Migrations
{
    /// <inheritdoc />
    public partial class SolucionCicloEnvios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Destinatarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DestinatarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinatarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destinatarios_Destinatarios_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Destinatarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EstadosEnvios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EstadosEnvioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosEnvios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadosEnvios_EstadosEnvios_EstadosEnvioId",
                        column: x => x.EstadosEnvioId,
                        principalTable: "EstadosEnvios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    DestinatarioId = table.Column<int>(type: "int", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    EstadosEnvioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartamentoDestino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envios_Destinatarios_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalTable: "Destinatarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envios_EstadosEnvios_EstadosEnvioId",
                        column: x => x.EstadosEnvioId,
                        principalTable: "EstadosEnvios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnvioId = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paquetes_Envios_EnvioId",
                        column: x => x.EnvioId,
                        principalTable: "Envios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paquetes_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ClienteId",
                table: "Clientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinatarios_DestinatarioId",
                table: "Destinatarios",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_ClienteId",
                table: "Envios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_DestinatarioId",
                table: "Envios",
                column: "DestinatarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_EstadosEnvioId",
                table: "Envios",
                column: "EstadosEnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_PaqueteId",
                table: "Envios",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosEnvios_EstadosEnvioId",
                table: "EstadosEnvios",
                column: "EstadosEnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_Paquetes_EnvioId",
                table: "Paquetes",
                column: "EnvioId");

            migrationBuilder.CreateIndex(
                name: "IX_Paquetes_PaqueteId",
                table: "Paquetes",
                column: "PaqueteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Paquetes_PaqueteId",
                table: "Envios",
                column: "PaqueteId",
                principalTable: "Paquetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Clientes_ClienteId",
                table: "Envios");

            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Destinatarios_DestinatarioId",
                table: "Envios");

            migrationBuilder.DropForeignKey(
                name: "FK_Envios_EstadosEnvios_EstadosEnvioId",
                table: "Envios");

            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Paquetes_PaqueteId",
                table: "Envios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Destinatarios");

            migrationBuilder.DropTable(
                name: "EstadosEnvios");

            migrationBuilder.DropTable(
                name: "Paquetes");

            migrationBuilder.DropTable(
                name: "Envios");
        }
    }
}
