using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.API.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estacionamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Placa = table.Column<string>(type: "TEXT", nullable: false),
                    HorarioChegada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HorarioSaida = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Duracao = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    TempoCobrado = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    PrecoHora = table.Column<decimal>(type: "TEXT", nullable: true),
                    ValorAPagar = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrecoPorHora = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preco", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estacionamento");

            migrationBuilder.DropTable(
                name: "Preco");
        }
    }
}
