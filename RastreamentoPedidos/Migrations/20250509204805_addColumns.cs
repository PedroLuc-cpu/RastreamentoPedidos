using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class addColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "login",
                table: "usuario",
                newName: "senhaConfirmacao");

            migrationBuilder.AlterColumn<bool>(
                name: "ativo",
                table: "usuario",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "true");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "funcao",
                table: "usuario",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "funcao",
                table: "usuario");

            migrationBuilder.RenameColumn(
                name: "senhaConfirmacao",
                table: "usuario",
                newName: "login");

            migrationBuilder.AlterColumn<bool>(
                name: "ativo",
                table: "usuario",
                type: "boolean",
                nullable: false,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
