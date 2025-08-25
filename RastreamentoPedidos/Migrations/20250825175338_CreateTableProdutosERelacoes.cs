using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProdutosERelacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produtoCategoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produtoId = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoCategoria", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "produtoEncargos",
                columns: table => new
                {
                    id_encargos = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProdutoId = table.Column<int>(type: "integer", nullable: false),
                    valorFrete = table.Column<double>(type: "double precision", nullable: false),
                    valorSeguro = table.Column<double>(type: "double precision", nullable: false),
                    valorDespesas = table.Column<double>(type: "double precision", nullable: false),
                    valorOutros = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoEncargos", x => x.id_encargos);
                });

            migrationBuilder.CreateTable(
                name: "produtoMarca",
                columns: table => new
                {
                    id_marca = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoMarca", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "produtoPeso",
                columns: table => new
                {
                    id_produtoPeso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProduto = table.Column<int>(type: "integer", nullable: false),
                    pesoBruto = table.Column<double>(type: "double precision", nullable: false),
                    pesoLiquido = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoPeso", x => x.id_produtoPeso);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id_produto = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: false),
                    codigoBarras = table.Column<string>(type: "text", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    unidadeMedida = table.Column<string>(type: "text", nullable: false),
                    precoVenda = table.Column<double>(type: "double precision", nullable: false),
                    precoCusto = table.Column<double>(type: "double precision", nullable: false),
                    estoqueAtual = table.Column<double>(type: "double precision", nullable: false),
                    estoqueMinimo = table.Column<double>(type: "double precision", nullable: false),
                    estoqueMaximo = table.Column<double>(type: "double precision", nullable: false),
                    estoqueReservado = table.Column<double>(type: "double precision", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    dataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    idCategoria = table.Column<int>(type: "integer", nullable: false),
                    idMarca = table.Column<int>(type: "integer", nullable: false),
                    urlImagem = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.id_produto);
                    table.ForeignKey(
                        name: "FK_produtos_produtoCategoria_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "produtoCategoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produtos_produtoEncargos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "produtoEncargos",
                        principalColumn: "id_encargos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produtos_produtoMarca_idMarca",
                        column: x => x.idMarca,
                        principalTable: "produtoMarca",
                        principalColumn: "id_marca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produtos_produtoPeso_id_produto",
                        column: x => x.id_produto,
                        principalTable: "produtoPeso",
                        principalColumn: "id_produtoPeso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_produtos_idCategoria",
                table: "produtos",
                column: "idCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_idMarca",
                table: "produtos",
                column: "idMarca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "produtoCategoria");

            migrationBuilder.DropTable(
                name: "produtoEncargos");

            migrationBuilder.DropTable(
                name: "produtoMarca");

            migrationBuilder.DropTable(
                name: "produtoPeso");
        }
    }
}
