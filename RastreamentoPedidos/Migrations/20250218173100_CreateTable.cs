using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_encomenda = table.Column<long>(type: "bigint", nullable: true),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.idCliente);
                });

            migrationBuilder.CreateTable(
                name: "tp_logradouro",
                columns: table => new
                {
                    idTpLogradouro = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "varchar", nullable: false),
                    sigla = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tp_logradouro", x => x.idTpLogradouro);
                });

            migrationBuilder.CreateTable(
                name: "uf",
                columns: table => new
                {
                    ifUF = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    sigla = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uf", x => x.ifUF);
                });

            migrationBuilder.CreateTable(
                name: "encomendas",
                columns: table => new
                {
                    idEncomenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    idCliente = table.Column<int>(type: "integer", nullable: false),
                    clienteidCliente = table.Column<int>(type: "integer", nullable: false),
                    data_pedido = table.Column<DateTime>(type: "timestamp", nullable: false),
                    descricao = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encomendas", x => x.idEncomenda);
                    table.ForeignKey(
                        name: "FK_encomendas_Clientes_clienteidCliente",
                        column: x => x.clienteidCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_encomendas_Clientes_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telefone",
                columns: table => new
                {
                    idTelefoneCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    prefixo = table.Column<string>(type: "varchar", nullable: false),
                    numero = table.Column<string>(type: "varchar", nullable: false),
                    idCliente = table.Column<int>(type: "integer", nullable: false),
                    padrao = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefone", x => x.idTelefoneCliente);
                    table.ForeignKey(
                        name: "FK_telefone_Clientes_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cidade",
                columns: table => new
                {
                    idCidade = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "varchar", nullable: false),
                    idUF = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cidade", x => x.idCidade);
                    table.ForeignKey(
                        name: "FK_cidade_uf_idUF",
                        column: x => x.idUF,
                        principalTable: "uf",
                        principalColumn: "ifUF",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "status_entrega",
                columns: table => new
                {
                    id_encomenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    status = table.Column<string>(type: "varchar", maxLength: 225, nullable: false),
                    decricao = table.Column<string>(type: "text", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_encomenda1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_entrega", x => x.id_encomenda);
                    table.ForeignKey(
                        name: "FK_status_entrega_encomendas_id_encomenda",
                        column: x => x.id_encomenda,
                        principalTable: "encomendas",
                        principalColumn: "idEncomenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    idEnderecoCliente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    idTpLogradouro = table.Column<long>(type: "bigint", nullable: false),
                    Complemento = table.Column<string>(type: "text", nullable: false),
                    bairro = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    numero = table.Column<string>(type: "varchar", nullable: false),
                    Rua = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    CEP = table.Column<string>(type: "varchar", nullable: false),
                    idCliente = table.Column<int>(type: "integer", nullable: false),
                    CidadeidCidade = table.Column<long>(type: "bigint", nullable: false),
                    EncomendaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.idEnderecoCliente);
                    table.ForeignKey(
                        name: "FK_endereco_Clientes_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_endereco_cidade_CidadeidCidade",
                        column: x => x.CidadeidCidade,
                        principalTable: "cidade",
                        principalColumn: "idCidade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_endereco_encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "encomendas",
                        principalColumn: "idEncomenda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_endereco_tp_logradouro_idTpLogradouro",
                        column: x => x.idTpLogradouro,
                        principalTable: "tp_logradouro",
                        principalColumn: "idTpLogradouro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cidade_idUF",
                table: "cidade",
                column: "idUF");

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_clienteidCliente",
                table: "encomendas",
                column: "clienteidCliente");

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_idCliente",
                table: "encomendas",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_CidadeidCidade",
                table: "endereco",
                column: "CidadeidCidade");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_EncomendaId",
                table: "endereco",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_idCliente",
                table: "endereco",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_idTpLogradouro",
                table: "endereco",
                column: "idTpLogradouro");

            migrationBuilder.CreateIndex(
                name: "IX_telefone_idCliente",
                table: "telefone",
                column: "idCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "status_entrega");

            migrationBuilder.DropTable(
                name: "telefone");

            migrationBuilder.DropTable(
                name: "cidade");

            migrationBuilder.DropTable(
                name: "tp_logradouro");

            migrationBuilder.DropTable(
                name: "encomendas");

            migrationBuilder.DropTable(
                name: "uf");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
