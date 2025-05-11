using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RastreamentoPedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class alterColumnStatusUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statusUsser",
                table: "AspNetUsers",
                newName: "StatusUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusUser",
                table: "AspNetUsers",
                newName: "statusUsser");
        }
    }
}
