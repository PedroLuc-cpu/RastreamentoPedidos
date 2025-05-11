using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoNomeColumnNomeCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeComplesto",
                table: "AspNetUsers",
                newName: "NomeCompleto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "AspNetUsers",
                newName: "NomeComplesto");
        }
    }
}
