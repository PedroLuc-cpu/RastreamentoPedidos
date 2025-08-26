using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigColunasNulosTProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "urlImagem",
                table: "produtos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<double>(
                name: "precoCusto",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "produtos",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<int>(
                name: "idMarca",
                table: "produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idCategoria",
                table: "produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueReservado",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMinimo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMaximo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueAtual",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "urlImagem",
                table: "produtos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "precoCusto",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "produtos",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idMarca",
                table: "produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idCategoria",
                table: "produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "estoqueReservado",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMinimo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMaximo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "estoqueAtual",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)",
                oldNullable: true);
        }
    }
}
