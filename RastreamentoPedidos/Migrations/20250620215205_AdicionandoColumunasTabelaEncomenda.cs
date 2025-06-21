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
                name: "FK_encomenda_auditoria_encomendas_idEncomenda",
                table: "encomenda_auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_pontos_parada_rotas_IdRota",
                table: "pontos_parada");

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
                name: "id_encomendaauditorias",
                table: "encomendas");

            migrationBuilder.RenameColumn(
                name: "idRota",
                table: "rotas",
                newName: "id_rota");

            migrationBuilder.RenameColumn(
                name: "idPontoParada",
                table: "pontos_parada",
                newName: "id_ponto_parada");

            migrationBuilder.RenameColumn(
                name: "IdRota",
                table: "pontos_parada",
                newName: "id_rota");

            migrationBuilder.RenameIndex(
                name: "IX_pontos_parada_IdRota",
                table: "pontos_parada",
                newName: "IX_pontos_parada_id_rota");

            migrationBuilder.RenameColumn(
                name: "data_pedido",
                table: "encomendas",
                newName: "data_encomenda");

            migrationBuilder.RenameColumn(
                name: "idEncomenda",
                table: "encomendas",
                newName: "id_encomenda");

            migrationBuilder.RenameColumn(
                name: "statusAtual",
                table: "encomenda_auditoria",
                newName: "status_atual");

            migrationBuilder.RenameColumn(
                name: "localOrigem",
                table: "encomenda_auditoria",
                newName: "local_origem");

            migrationBuilder.RenameColumn(
                name: "localDestino",
                table: "encomenda_auditoria",
                newName: "local_destino");

            migrationBuilder.RenameColumn(
                name: "descricaoEvento",
                table: "encomenda_auditoria",
                newName: "descricao_evento");

            migrationBuilder.RenameColumn(
                name: "dataRegistro",
                table: "encomenda_auditoria",
                newName: "data_registro");

            migrationBuilder.RenameColumn(
                name: "dataHoraEvento",
                table: "encomenda_auditoria",
                newName: "data_hora_evento");

            migrationBuilder.RenameColumn(
                name: "idEncomendaAuditoria",
                table: "encomenda_auditoria",
                newName: "id_encomenda_auditoria");

            migrationBuilder.RenameColumn(
                name: "statusEntregas",
                table: "encomenda_auditoria",
                newName: "status_entregas");

            migrationBuilder.RenameColumn(
                name: "idEncomenda",
                table: "encomenda_auditoria",
                newName: "id_encomenda");

            migrationBuilder.RenameIndex(
                name: "IX_encomenda_auditoria_idEncomenda",
                table: "encomenda_auditoria",
                newName: "IX_encomenda_auditoria_id_encomenda");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "status_entregas",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "rotas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<int>(
                name: "id_rota",
                table: "encomendas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "id_cliente",
                table: "encomendas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "encomendas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AddColumn<int>(
                name: "RotaId1",
                table: "encomendas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_status_encomenda",
                table: "encomendas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EncomendaId1",
                table: "encomenda_auditoria",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_id_status_encomenda",
                table: "encomendas",
                column: "id_status_encomenda");

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_RotaId1",
                table: "encomendas",
                column: "RotaId1");

            migrationBuilder.CreateIndex(
                name: "IX_encomenda_auditoria_EncomendaId1",
                table: "encomenda_auditoria",
                column: "EncomendaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_encomenda_auditoria_encomendas_EncomendaId1",
                table: "encomenda_auditoria",
                column: "EncomendaId1",
                principalTable: "encomendas",
                principalColumn: "id_encomenda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomenda_auditoria_encomendas_id_encomenda",
                table: "encomenda_auditoria",
                column: "id_encomenda",
                principalTable: "encomendas",
                principalColumn: "id_encomenda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_rotas_RotaId1",
                table: "encomendas",
                column: "RotaId1",
                principalTable: "rotas",
                principalColumn: "id_rota");

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_status_entregas_id",
                table: "encomendas",
                column: "id_status_encomenda",
                principalTable: "status_entregas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pontos_parada_rotas_id_rota",
                table: "pontos_parada",
                column: "id_rota",
                principalTable: "rotas",
                principalColumn: "id_rota",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_encomenda_auditoria_encomendas_EncomendaId1",
                table: "encomenda_auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_encomenda_auditoria_encomendas_id_encomenda",
                table: "encomenda_auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_rotas_RotaId1",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_encomendas_status_entregas_id",
                table: "encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_pontos_parada_rotas_id_rota",
                table: "pontos_parada");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_id_status_encomenda",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomendas_RotaId1",
                table: "encomendas");

            migrationBuilder.DropIndex(
                name: "IX_encomenda_auditoria_EncomendaId1",
                table: "encomenda_auditoria");

            migrationBuilder.DropColumn(
                name: "RotaId1",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "id_status_encomenda",
                table: "encomendas");

            migrationBuilder.DropColumn(
                name: "EncomendaId1",
                table: "encomenda_auditoria");

            migrationBuilder.RenameColumn(
                name: "id_rota",
                table: "rotas",
                newName: "idRota");

            migrationBuilder.RenameColumn(
                name: "id_ponto_parada",
                table: "pontos_parada",
                newName: "idPontoParada");

            migrationBuilder.RenameColumn(
                name: "id_rota",
                table: "pontos_parada",
                newName: "IdRota");

            migrationBuilder.RenameIndex(
                name: "IX_pontos_parada_id_rota",
                table: "pontos_parada",
                newName: "IX_pontos_parada_IdRota");

            migrationBuilder.RenameColumn(
                name: "data_encomenda",
                table: "encomendas",
                newName: "data_pedido");

            migrationBuilder.RenameColumn(
                name: "id_encomenda",
                table: "encomendas",
                newName: "idEncomenda");

            migrationBuilder.RenameColumn(
                name: "status_atual",
                table: "encomenda_auditoria",
                newName: "statusAtual");

            migrationBuilder.RenameColumn(
                name: "local_origem",
                table: "encomenda_auditoria",
                newName: "localOrigem");

            migrationBuilder.RenameColumn(
                name: "local_destino",
                table: "encomenda_auditoria",
                newName: "localDestino");

            migrationBuilder.RenameColumn(
                name: "descricao_evento",
                table: "encomenda_auditoria",
                newName: "descricaoEvento");

            migrationBuilder.RenameColumn(
                name: "data_registro",
                table: "encomenda_auditoria",
                newName: "dataRegistro");

            migrationBuilder.RenameColumn(
                name: "data_hora_evento",
                table: "encomenda_auditoria",
                newName: "dataHoraEvento");

            migrationBuilder.RenameColumn(
                name: "id_encomenda_auditoria",
                table: "encomenda_auditoria",
                newName: "idEncomendaAuditoria");

            migrationBuilder.RenameColumn(
                name: "status_entregas",
                table: "encomenda_auditoria",
                newName: "statusEntregas");

            migrationBuilder.RenameColumn(
                name: "id_encomenda",
                table: "encomenda_auditoria",
                newName: "idEncomenda");

            migrationBuilder.RenameIndex(
                name: "IX_encomenda_auditoria_id_encomenda",
                table: "encomenda_auditoria",
                newName: "IX_encomenda_auditoria_idEncomenda");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "status_entregas",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "rotas",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<int>(
                name: "id_rota",
                table: "encomendas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "id_cliente",
                table: "encomendas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "encomendas",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

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

            migrationBuilder.AddColumn<int>(
                name: "id_encomendaauditorias",
                table: "encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_encomendas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_encomenda_auditoria_encomendas_idEncomenda",
                table: "encomenda_auditoria",
                column: "idEncomenda",
                principalTable: "encomendas",
                principalColumn: "idEncomenda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_encomendas_status_entregas_StatusEncomendaCodigo",
                table: "encomendas",
                column: "StatusEncomendaCodigo",
                principalTable: "status_entregas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pontos_parada_rotas_IdRota",
                table: "pontos_parada",
                column: "IdRota",
                principalTable: "rotas",
                principalColumn: "idRota",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
