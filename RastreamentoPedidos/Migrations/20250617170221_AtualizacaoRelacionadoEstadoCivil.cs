using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoRelacionadoEstadoCivil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estadoCivil_Clientes_idcliente",
                table: "estadoCivil");

            migrationBuilder.DropIndex(
                name: "IX_estadoCivil_idcliente",
                table: "estadoCivil");

            migrationBuilder.DropColumn(
                name: "idcliente",
                table: "estadoCivil");

            migrationBuilder.AddForeignKey(
                name: "FK_estadoCivil_Clientes_idestadocivil",
                table: "estadoCivil",
                column: "idestadocivil",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estadoCivil_Clientes_idestadocivil",
                table: "estadoCivil");

            migrationBuilder.AddColumn<int>(
                name: "idcliente",
                table: "estadoCivil",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_estadoCivil_idcliente",
                table: "estadoCivil",
                column: "idcliente",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_estadoCivil_Clientes_idcliente",
                table: "estadoCivil",
                column: "idcliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
