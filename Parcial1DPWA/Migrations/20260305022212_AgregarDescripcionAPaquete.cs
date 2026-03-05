using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial1DPWA.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDescripcionAPaquete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Paquetes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Paquetes");
        }
    }
}
