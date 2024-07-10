using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Preco",
                table: "Preco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estacionamento",
                table: "Estacionamento");

            migrationBuilder.RenameTable(
                name: "Preco",
                newName: "Precos");

            migrationBuilder.RenameTable(
                name: "Estacionamento",
                newName: "Estacionamentos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Precos",
                table: "Precos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estacionamentos",
                table: "Estacionamentos",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Precos",
                table: "Precos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estacionamentos",
                table: "Estacionamentos");

            migrationBuilder.RenameTable(
                name: "Precos",
                newName: "Preco");

            migrationBuilder.RenameTable(
                name: "Estacionamentos",
                newName: "Estacionamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preco",
                table: "Preco",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estacionamento",
                table: "Estacionamento",
                column: "Id");
        }
    }
}
