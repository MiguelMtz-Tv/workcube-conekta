using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupones_Clientes_IdCliente",
                table: "Cupones");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupones_Servicios_IdServicio",
                table: "Cupones");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupones_Clientes_IdCliente",
                table: "Cupones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cupones_Servicios_IdServicio",
                table: "Cupones",
                column: "IdServicio",
                principalTable: "Servicios",
                principalColumn: "IdServicio",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cupones_Clientes_IdCliente",
                table: "Cupones");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupones_Servicios_IdServicio",
                table: "Cupones");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupones_Clientes_IdCliente",
                table: "Cupones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupones_Servicios_IdServicio",
                table: "Cupones",
                column: "IdServicio",
                principalTable: "Servicios",
                principalColumn: "IdServicio");
        }
    }
}
