using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarENovosCamposDaTabelaCriente : Migration
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
                name: "decricao",
                table: "status_entrega",
                newName: "Decricao");

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
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "clienteidCliente",
                table: "encomendas",
                newName: "ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_idCliente",
                table: "encomendas",
                newName: "IX_encomendas_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_clienteidCliente",
                table: "encomendas",
                newName: "IX_encomendas_ClienteIdCliente");

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
                name: "estadoCivil",
                columns: table => new
                {
                    idestadocivil = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    idcliente = table.Column<int>(type: "integer", nullable: false),
                    EstadoCivilDescricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estadoCivil", x => x.idestadocivil);
                    table.ForeignKey(
                        name: "FK_estadoCivil_Clientes_idcliente",
                        column: x => x.idcliente,
                        principalTable: "Clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_estadoCivil_idcliente",
                table: "estadoCivil",
                column: "idcliente",
                unique: true);

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
                name: "FK_encomendas_Clientes_ClienteIdCliente",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_Clientes_IdCliente",
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
                name: "estadoCivil");

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
                name: "Decricao",
                table: "status_entrega",
                newName: "decricao");

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
                name: "IdCliente",
                table: "encomendas",
                newName: "idCliente");

            migrationBuilder.RenameColumn(
                name: "ClienteIdCliente",
                table: "encomendas",
                newName: "clienteidCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_IdCliente",
                table: "encomendas",
                newName: "IX_encomendas_idCliente");

            migrationBuilder.RenameIndex(
                name: "IX_encomendas_ClienteIdCliente",
                table: "encomendas",
                newName: "IX_encomendas_clienteidCliente");

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
