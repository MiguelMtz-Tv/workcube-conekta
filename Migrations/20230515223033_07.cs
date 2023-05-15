using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupon_Clientes_IdCliente",
                table: "Cupon");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupon_servicios_IdServicio",
                table: "Cupon");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_Clientes_IdCliente",
                table: "Cupon",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_servicios_IdServicio",
                table: "Cupon",
                column: "IdServicio",
                principalTable: "servicios",
                principalColumn: "IdServicio",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupon_Clientes_IdCliente",
                table: "Cupon");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupon_servicios_IdServicio",
                table: "Cupon");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_Clientes_IdCliente",
                table: "Cupon",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_servicios_IdServicio",
                table: "Cupon",
                column: "IdServicio",
                principalTable: "servicios",
                principalColumn: "IdServicio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
