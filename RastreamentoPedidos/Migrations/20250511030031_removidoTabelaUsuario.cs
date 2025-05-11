using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class removidoTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "usuarios");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "usuarios",
                newName: "nomeUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "nomeUsuario",
                table: "usuario",
                newName: "nome");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "idUsuario");
        }
    }
}
