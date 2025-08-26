using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigColunasDoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoCategoria_idCategoria",
                table: "produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoEncargos_id_produto",
                table: "produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoMarca_idMarca",
                table: "produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoPeso_id_produto",
                table: "produtos");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "produtoEncargos",
                newName: "IdProduto");

            migrationBuilder.AlterColumn<string>(
                name: "unidadeMedida",
                table: "produtos",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<double>(
                name: "precoVenda",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "precoCusto",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "produtos",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "produtos",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "idMarca",
                table: "produtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "idCategoria",
                table: "produtos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueReservado",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMinimo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMaximo",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueAtual",
                table: "produtos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataCadastro",
                table: "produtos",
                type: "TIMESTAMP",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "codigoBarras",
                table: "produtos",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "produtos",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "ativo",
                table: "produtos",
                type: "boolean",
                nullable: false,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "id_produto",
                table: "produtos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_produtoPeso_IdProduto",
                table: "produtoPeso",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produtoEncargos_IdProduto",
                table: "produtoEncargos",
                column: "IdProduto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_produtoEncargos_produtos_IdProduto",
                table: "produtoEncargos",
                column: "IdProduto",
                principalTable: "produtos",
                principalColumn: "id_produto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produtoPeso_produtos_IdProduto",
                table: "produtoPeso",
                column: "IdProduto",
                principalTable: "produtos",
                principalColumn: "id_produto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoCategoria_idCategoria",
                table: "produtos",
                column: "idCategoria",
                principalTable: "produtoCategoria",
                principalColumn: "id_categoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoMarca_idMarca",
                table: "produtos",
                column: "idMarca",
                principalTable: "produtoMarca",
                principalColumn: "id_marca",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtoEncargos_produtos_IdProduto",
                table: "produtoEncargos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtoPeso_produtos_IdProduto",
                table: "produtoPeso");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoCategoria_idCategoria",
                table: "produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_produtoMarca_idMarca",
                table: "produtos");

            migrationBuilder.DropIndex(
                name: "IX_produtoPeso_IdProduto",
                table: "produtoPeso");

            migrationBuilder.DropIndex(
                name: "IX_produtoEncargos_IdProduto",
                table: "produtoEncargos");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "produtoEncargos",
                newName: "ProdutoId");

            migrationBuilder.AlterColumn<string>(
                name: "unidadeMedida",
                table: "produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AlterColumn<double>(
                name: "precoVenda",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "precoCusto",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<int>(
                name: "idMarca",
                table: "produtos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idCategoria",
                table: "produtos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueReservado",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMinimo",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueMaximo",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "estoqueAtual",
                table: "produtos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataCadastro",
                table: "produtos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<string>(
                name: "codigoBarras",
                table: "produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "produtos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<bool>(
                name: "ativo",
                table: "produtos",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "true");

            migrationBuilder.AlterColumn<int>(
                name: "id_produto",
                table: "produtos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoCategoria_idCategoria",
                table: "produtos",
                column: "idCategoria",
                principalTable: "produtoCategoria",
                principalColumn: "id_categoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoEncargos_id_produto",
                table: "produtos",
                column: "id_produto",
                principalTable: "produtoEncargos",
                principalColumn: "id_encargos",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoMarca_idMarca",
                table: "produtos",
                column: "idMarca",
                principalTable: "produtoMarca",
                principalColumn: "id_marca",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_produtoPeso_id_produto",
                table: "produtos",
                column: "id_produto",
                principalTable: "produtoPeso",
                principalColumn: "id_produtoPeso",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
