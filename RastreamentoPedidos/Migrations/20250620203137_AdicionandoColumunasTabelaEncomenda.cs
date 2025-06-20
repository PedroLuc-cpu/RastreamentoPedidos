using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColumunasTabelaEncomenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.RenameColumn(
                name: "IdStatusEncomenda",
                table: "encomendas",
                newName: "idStatusEncomenda");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "status_entregas",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "idStatusEncomenda",
                table: "encomendas",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_idStatusEncomenda",
                table: "encomendas",
                column: "idStatusEncomenda");

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_status_entregas_idStatusEncomenda",
                table: "encomendas",
                column: "idStatusEncomenda",
                principalTable: "status_entregas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_status_entregas_idStatusEncomenda",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_idStatusEncomenda",
                table: "encomendas");

            migrationBuilder.RenameColumn(
                name: "idStatusEncomenda",
                table: "encomendas",
                newName: "IdStatusEncomenda");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "status_entregas",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<int>(
                name: "IdStatusEncomenda",
                table: "encomendas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "StatusEncomendaCodigo",
                table: "encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo",
                principalTable: "status_entregas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
