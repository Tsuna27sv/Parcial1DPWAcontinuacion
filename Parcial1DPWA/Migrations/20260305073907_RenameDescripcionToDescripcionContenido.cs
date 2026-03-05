using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial1DPWA.Migrations
{
    /// <inheritdoc />
    public partial class RenameDescripcionToDescripcionContenido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Paquetes");

            migrationBuilder.AddColumn<string>(
                name: "DescripcionContenido",
                table: "Paquetes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionContenido",
                table: "Paquetes");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Paquetes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
