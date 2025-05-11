using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoColumnNomeCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeComplesto",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeComplesto",
                table: "AspNetUsers");
        }
    }
}
