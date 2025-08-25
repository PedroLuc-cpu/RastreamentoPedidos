using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoColunaProdutoPesoDataAtualizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dtPesoAtualizado",
                table: "produtoPeso",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dtPesoAtualizado",
                table: "produtoPeso");
        }
    }
}
