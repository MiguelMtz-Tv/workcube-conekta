using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarjetas_Clientes_IdCliente",
                table: "Tarjetas");

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdStripeCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<long>(type: "bigint", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pagos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Servicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdCliente",
                table: "Pagos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdServicio",
                table: "Pagos",
                column: "IdServicio");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarjetas_Clientes_IdCliente",
                table: "Tarjetas",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarjetas_Clientes_IdCliente",
                table: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarjetas_Clientes_IdCliente",
                table: "Tarjetas",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
