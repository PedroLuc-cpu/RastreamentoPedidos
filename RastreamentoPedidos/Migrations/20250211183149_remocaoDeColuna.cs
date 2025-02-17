using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.Migrations
{
    /// <inheritdoc />
    public partial class remocaoDeColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_encomenda",
                table: "clientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_encomenda",
                table: "clientes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
