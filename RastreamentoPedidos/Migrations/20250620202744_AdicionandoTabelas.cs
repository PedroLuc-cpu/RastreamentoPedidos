﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_clienteidCliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_idCliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_Clientes_idCliente",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_cidade_CidadeidCidade",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_tp_logradouro_idTpLogradouro",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_telefone_Clientes_idCliente",
                table: "telefone");

            migrationBuilder.DropTable(
                name: "status_entrega");

            migrationBuilder.DropColumn(
                name: "id_encomenda",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "idCliente",
                table: "telefone",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_telefone_idCliente",
                table: "telefone",
                newName: "IX_telefone_IdCliente");

            migrationBuilder.RenameColumn(
                name: "idTpLogradouro",
                table: "endereco",
                newName: "IdTpLogradouro");

            migrationBuilder.RenameColumn(
                name: "idCliente",
                table: "endereco",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "CidadeidCidade",
                table: "endereco",
                newName: "CidadeIdCidade");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_idTpLogradouro",
                table: "endereco",
                newName: "IX_endereco_IdTpLogradouro");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_idCliente",
                table: "endereco",
                newName: "IX_endereco_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_CidadeidCidade",
                table: "endereco",
                newName: "IX_endereco_CidadeIdCidade");

            migrationBuilder.RenameColumn(
                name: "idCliente",
                table: "encomendas",
                newName: "id_cliente");

            migrationBuilder.RenameColumn(
                name: "clienteidCliente",
                table: "encomendas",
                newName: "id_rota");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_idCliente",
                table: "encomendas",
                newName: "IX_encomendas_id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_clienteidCliente",
                table: "encomendas",
                newName: "IX_encomendas_id_rota");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Clientes",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Clientes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "documento",
                table: "Clientes",
                newName: "Documento");

            migrationBuilder.AlterColumn<int>(
                name: "idTpLogradouro",
                table: "tp_logradouro",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IdTpLogradouro",
                table: "endereco",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CidadeIdCidade",
                table: "endereco",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "idEnderecoCliente",
                table: "endereco",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "IdStatusEncomenda",
                table: "encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusEncomendaCodigo",
                table: "encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "id_encomendaauditorias",
                table: "encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Clientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EstadoCivilId",
                table: "Clientes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEncomenda",
                table: "Clientes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sexo",
                table: "Clientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "idCidade",
                table: "cidade",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

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
                        name: "FK_encomenda_auditoria_encomendas_idEncomenda",
                        column: x => x.idEncomenda,
                        principalTable: "encomendas",
                        principalColumn: "idEncomenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estadoCivil",
                columns: table => new
                {
                    idestadocivil = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EstadoCivilDescricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadoCivil", x => x.idestadocivil);
                    table.ForeignKey(
                        name: "FK_estadoCivil_Clientes_idestadocivil",
                        column: x => x.idestadocivil,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
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
                name: "status_entregas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_entregas", x => x.id);
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
                    ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pontos_parada", x => x.idPontoParada);
                    table.ForeignKey(
                        name: "FK_pontos_parada_rotas_IdRota",
                        column: x => x.IdRota,
                        principalTable: "rotas",
                        principalColumn: "idRota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_codigo_rastreamento",
                table: "encomendas",
                column: "codigo_rastreamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_encomenda_auditoria_idEncomenda",
                table: "encomenda_auditoria",
                column: "idEncomenda");

            migrationBuilder.CreateIndex(
                name: "IX_pontos_parada_IdRota",
                table: "pontos_parada",
                column: "IdRota");

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_Clientes_id_cliente",
                table: "encomendas",
                column: "id_cliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_rotas_id_rota",
                table: "encomendas",
                column: "id_rota",
                principalTable: "rotas",
                principalColumn: "idRota",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo",
                principalTable: "status_entregas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_Clientes_IdCliente",
                table: "endereco",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_cidade_CidadeIdCidade",
                table: "endereco",
                column: "CidadeIdCidade",
                principalTable: "cidade",
                principalColumn: "idCidade",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_tp_logradouro_IdTpLogradouro",
                table: "endereco",
                column: "IdTpLogradouro",
                principalTable: "tp_logradouro",
                principalColumn: "idTpLogradouro",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_telefone_Clientes_IdCliente",
                table: "telefone",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_id_cliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_rotas_id_rota",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_Clientes_IdCliente",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_cidade_CidadeIdCidade",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_tp_logradouro_IdTpLogradouro",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_telefone_Clientes_IdCliente",
                table: "telefone");

            migrationBuilder.DropTable(
                name: "encomenda_auditoria");

            migrationBuilder.DropTable(
                name: "estadoCivil");

            migrationBuilder.DropTable(
                name: "pontos_parada");

            migrationBuilder.DropTable(
                name: "status_entregas");

            migrationBuilder.DropTable(
                name: "rotas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_codigo_rastreamento",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "IdStatusEncomenda",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "StatusEncomendaCodigo",
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
                name: "id_encomendaauditorias",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EstadoCivilId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IdEncomenda",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "telefone",
                newName: "idCliente");

            migrationBuilder.RenameIndex(
                name: "IX_telefone_IdCliente",
                table: "telefone",
                newName: "IX_telefone_idCliente");

            migrationBuilder.RenameColumn(
                name: "IdTpLogradouro",
                table: "endereco",
                newName: "idTpLogradouro");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "endereco",
                newName: "idCliente");

            migrationBuilder.RenameColumn(
                name: "CidadeIdCidade",
                table: "endereco",
                newName: "CidadeidCidade");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_IdTpLogradouro",
                table: "endereco",
                newName: "IX_endereco_idTpLogradouro");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_IdCliente",
                table: "endereco",
                newName: "IX_endereco_idCliente");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_CidadeIdCidade",
                table: "endereco",
                newName: "IX_endereco_CidadeidCidade");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "encomendas",
                newName: "idCliente");

            migrationBuilder.RenameColumn(
                name: "id_rota",
                table: "encomendas",
                newName: "clienteidCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_id_rota",
                table: "encomendas",
                newName: "IX_encomendas_clienteidCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_id_cliente",
                table: "encomendas",
                newName: "IX_encomendas_idCliente");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Clientes",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Clientes",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Documento",
                table: "Clientes",
                newName: "documento");

            migrationBuilder.AlterColumn<long>(
                name: "idTpLogradouro",
                table: "tp_logradouro",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<long>(
                name: "idTpLogradouro",
                table: "endereco",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CidadeidCidade",
                table: "endereco",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "idEnderecoCliente",
                table: "endereco",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<long>(
                name: "id_encomenda",
                table: "Clientes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "idCidade",
                table: "cidade",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateTable(
                name: "status_entrega",
                columns: table => new
                {
                    id_encomenda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp", nullable: false),
                    decricao = table.Column<string>(type: "text", nullable: false),
                    id_encomenda1 = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "varchar", maxLength: 225, nullable: false)
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
                name: "FK_encomendas_Clientes_clienteidCliente",
                table: "encomendas",
                column: "clienteidCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_Clientes_idCliente",
                table: "encomendas",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_Clientes_idCliente",
                table: "endereco",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_cidade_CidadeidCidade",
                table: "endereco",
                column: "CidadeidCidade",
                principalTable: "cidade",
                principalColumn: "idCidade",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_tp_logradouro_idTpLogradouro",
                table: "endereco",
                column: "idTpLogradouro",
                principalTable: "tp_logradouro",
                principalColumn: "idTpLogradouro",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_telefone_Clientes_idCliente",
                table: "telefone",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
