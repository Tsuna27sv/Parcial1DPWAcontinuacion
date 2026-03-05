using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial1DPWA.Migrations
{
    /// <inheritdoc />
    public partial class HacerEnvioIdOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paquetes_Envios_EnvioId",
                table: "Paquetes");

            migrationBuilder.AlterColumn<int>(
                name: "EnvioId",
                table: "Paquetes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Paquetes_Envios_EnvioId",
                table: "Paquetes",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paquetes_Envios_EnvioId",
                table: "Paquetes");

            migrationBuilder.AlterColumn<int>(
                name: "EnvioId",
                table: "Paquetes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paquetes_Envios_EnvioId",
                table: "Paquetes",
                column: "EnvioId",
                principalTable: "Envios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
