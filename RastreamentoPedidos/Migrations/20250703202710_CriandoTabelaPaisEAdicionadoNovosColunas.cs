using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaPaisEAdicionadoNovosColunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_endereco_Clientes_IdCliente",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_encomendas_EncomendaId",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_tp_logradouro_IdTpLogradouro",
                table: "endereco");

            migrationBuilder.DropIndex(
                name: "IX_endereco_IdCliente",
                table: "endereco");

            migrationBuilder.RenameColumn(
                name: "IdTpLogradouro",
                table: "endereco",
                newName: "idTpLogradouro");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "endereco",
                newName: "idPessoa");

            migrationBuilder.RenameColumn(
                name: "EncomendaId",
                table: "endereco",
                newName: "idEncomenda");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_IdTpLogradouro",
                table: "endereco",
                newName: "IX_endereco_idTpLogradouro");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_EncomendaId",
                table: "endereco",
                newName: "IX_endereco_idEncomenda");

            migrationBuilder.AddColumn<int>(
                name: "CodUf",
                table: "uf",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPais",
                table: "uf",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "uf",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "idCidade",
                table: "endereco",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "cidade",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodIbge",
                table: "cidade",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    idPais = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    sigla = table.Column<string>(type: "varchar", maxLength: 3, nullable: false),
                    cod_bcb = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pais", x => x.idPais);
                });

            migrationBuilder.CreateIndex(
                name: "IX_uf_IdPais",
                table: "uf",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_idCidade",
                table: "endereco",
                column: "idCidade");

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_Clientes_idCidade",
                table: "endereco",
                column: "idCidade",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_encomendas_idEncomenda",
                table: "endereco",
                column: "idEncomenda",
                principalTable: "encomendas",
                principalColumn: "id_encomenda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_tp_logradouro_idTpLogradouro",
                table: "endereco",
                column: "idTpLogradouro",
                principalTable: "tp_logradouro",
                principalColumn: "idTpLogradouro",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_uf_pais_IdPais",
                table: "uf",
                column: "IdPais",
                principalTable: "pais",
                principalColumn: "idPais",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_endereco_Clientes_idCidade",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_encomendas_idEncomenda",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_tp_logradouro_idTpLogradouro",
                table: "endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_uf_pais_IdPais",
                table: "uf");

            migrationBuilder.DropTable(
                name: "pais");

            migrationBuilder.DropIndex(
                name: "IX_uf_IdPais",
                table: "uf");

            migrationBuilder.DropIndex(
                name: "IX_endereco_idCidade",
                table: "endereco");

            migrationBuilder.DropColumn(
                name: "CodUf",
                table: "uf");

            migrationBuilder.DropColumn(
                name: "IdPais",
                table: "uf");

            migrationBuilder.DropColumn(
                name: "nome",
                table: "uf");

            migrationBuilder.DropColumn(
                name: "idCidade",
                table: "endereco");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "cidade");

            migrationBuilder.DropColumn(
                name: "CodIbge",
                table: "cidade");

            migrationBuilder.RenameColumn(
                name: "idTpLogradouro",
                table: "endereco",
                newName: "IdTpLogradouro");

            migrationBuilder.RenameColumn(
                name: "idPessoa",
                table: "endereco",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "idEncomenda",
                table: "endereco",
                newName: "EncomendaId");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_idTpLogradouro",
                table: "endereco",
                newName: "IX_endereco_IdTpLogradouro");

            migrationBuilder.RenameIndex(
                name: "IX_endereco_idEncomenda",
                table: "endereco",
                newName: "IX_endereco_EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_IdCliente",
                table: "endereco",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_Clientes_IdCliente",
                table: "endereco",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "idCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_encomendas_EncomendaId",
                table: "endereco",
                column: "EncomendaId",
                principalTable: "encomendas",
                principalColumn: "id_encomenda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_tp_logradouro_IdTpLogradouro",
                table: "endereco",
                column: "IdTpLogradouro",
                principalTable: "tp_logradouro",
                principalColumn: "idTpLogradouro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
