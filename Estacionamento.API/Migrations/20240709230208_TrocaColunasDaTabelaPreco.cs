using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estacionamento.API.Migrations
{
    /// <inheritdoc />
    public partial class TrocaColunasDaTabelaPreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Preco");

            migrationBuilder.DropColumn(
                name: "PrecoPorHora",
                table: "Preco");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Preco");

            migrationBuilder.AddColumn<DateTime>(
                name: "FimVigencia",
                table: "Preco",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InicioVigencia",
                table: "Preco",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "ValorHoraAdicional",
                table: "Preco",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorHoraInicial",
                table: "Preco",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FimVigencia",
                table: "Preco");

            migrationBuilder.DropColumn(
                name: "InicioVigencia",
                table: "Preco");

            migrationBuilder.DropColumn(
                name: "ValorHoraAdicional",
                table: "Preco");

            migrationBuilder.DropColumn(
                name: "ValorHoraInicial",
                table: "Preco");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Preco",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoPorHora",
                table: "Preco",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Preco",
                type: "TEXT",
                nullable: true);
        }
    }
}
