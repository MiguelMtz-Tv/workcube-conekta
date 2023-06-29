﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumeroContrato",
                table: "Clientes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_NumeroContrato",
                table: "Clientes",
                column: "NumeroContrato",
                unique: true,
                filter: "[NumeroContrato] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_NumeroContrato",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroContrato",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
