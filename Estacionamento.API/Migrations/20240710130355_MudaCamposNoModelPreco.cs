using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.API.Migrations
{
    /// <inheritdoc />
    public partial class MudaCamposNoModelPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorHoraAdicional",
                table: "Precos");

            migrationBuilder.RenameColumn(
                name: "ValorHoraInicial",
                table: "Precos",
                newName: "ValorHora");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorHora",
                table: "Precos",
                newName: "ValorHoraInicial");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorHoraAdicional",
                table: "Precos",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
