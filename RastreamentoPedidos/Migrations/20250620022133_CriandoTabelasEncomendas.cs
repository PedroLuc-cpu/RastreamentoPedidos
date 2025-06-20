using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelasEncomendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_ClienteIdCliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_IdCliente",
                table: "encomendas");

            migrationBuilder.DropTable(
                name: "status_entrega");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "encomendas",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "ClienteIdCliente",
                table: "encomendas",
                newName: "id_rota");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_IdCliente",
                table: "encomendas",
                newName: "IX_encomendas_id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_ClienteIdCliente",
                table: "encomendas",
                newName: "IX_encomendas_id_rota");

            migrationBuilder.AddColumn<string>(
                name: "codigo_rastreamento",
                table: "encomendas",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao",
                table: "encomendas",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_previsao",
                table: "encomendas",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "id_status_encomenda",
                table: "encomendas",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "encomenda_auditoria",
                columns: table => new
                {
                    idEncomendaAuditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    idEncomenda = table.Column<int>(type: "integer", nullable: false),
                    dataHoraEvento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    localOrigem = table.Column<string>(type: "varchar(255)", nullable: false),
                    localDestino = table.Column<string>(type: "varchar(255)", nullable: false),
                    statusEntregas = table.Column<string>(type: "varchar(50)", nullable: false),
                    statusAtual = table.Column<string>(type: "varchar(50)", nullable: false),
                    descricaoEvento = table.Column<string>(type: "text", nullable: false),
                    responsavel = table.Column<string>(type: "varchar(100)", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: false),
                    dataRegistro = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encomenda_auditoria", x => x.idEncomendaAuditoria);
                    table.ForeignKey(
                        name: "FK_EncomendaAuditoria_Encomenda",
                        column: x => x.idEncomenda,
                        principalTable: "encomendas",
                        principalColumn: "idEncomenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rotas",
                columns: table => new
                {
                    idRota = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    descricao = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rotas", x => x.idRota);
                });

            migrationBuilder.CreateTable(
                name: "StatusEntregas",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEntregas", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "pontos_parada",
                columns: table => new
                {
                    idPontoParada = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdRota = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    localizacao = table.Column<string>(type: "varchar(255)", nullable: false),
                    ordem = table.Column<int>(type: "integer", nullable: false),
                    RotaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pontos_parada", x => x.idPontoParada);
                    table.ForeignKey(
                        name: "FK_Rota_PontoParada",
                        column: x => x.IdRota,
                        principalTable: "rotas",
                        principalColumn: "idRota",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pontos_parada_rotas_RotaId",
                        column: x => x.RotaId,
                        principalTable: "rotas",
                        principalColumn: "idRota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodigoRastreamento",
                table: "encomendas",
                column: "codigo_rastreamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_id_status_encomenda",
                table: "encomendas",
                column: "id_status_encomenda");

            migrationBuilder.CreateIndex(
                name: "IX_encomenda_auditoria_idEncomenda",
                table: "encomenda_auditoria",
                column: "idEncomenda");

            migrationBuilder.CreateIndex(
                name: "IX_pontos_parada_IdRota",
                table: "pontos_parada",
                column: "IdRota");

            migrationBuilder.CreateIndex(
                name: "IX_pontos_parada_RotaId",
                table: "pontos_parada",
                column: "RotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_Clientes_id_cliente",
                table: "encomendas",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_StatusEntregas_id_status_encomenda",
                table: "encomendas",
                column: "id_status_encomenda",
                principalTable: "StatusEntregas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_rotas_id_rota",
                table: "encomendas",
                column: "id_rota",
                principalTable: "rotas",
                principalColumn: "idRota",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_id_cliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_StatusEntregas_id_status_encomenda",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_rotas_id_rota",
                table: "encomendas");

            migrationBuilder.DropTable(
                name: "encomenda_auditoria");

            migrationBuilder.DropTable(
                name: "pontos_parada");

            migrationBuilder.DropTable(
                name: "StatusEntregas");

            migrationBuilder.DropTable(
                name: "rotas");

            migrationBuilder.DropIndex(
                name: "IX_CodigoRastreamento",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_id_status_encomenda",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "codigo_rastreamento",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "data_criacao",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "data_previsao",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "id_status_encomenda",
                table: "encomendas");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "encomendas",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "id_rota",
                table: "encomendas",
                newName: "ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_id_rota",
                table: "encomendas",
                newName: "IX_encomendas_ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_id_cliente",
                table: "encomendas",
                newName: "IX_encomendas_IdCliente");

            migrationBuilder.CreateTable(
                name: "status_entrega",
                columns: table => new
                {
                    id_encomenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Decricao = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "varchar", maxLength: 225, nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_Clientes_ClienteIdCliente",
                table: "encomendas",
                column: "ClienteIdCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_Clientes_IdCliente",
                table: "encomendas",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
