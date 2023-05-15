using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Periodo_IdPeriodo",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Periodo",
                table: "Periodo");

            migrationBuilder.RenameTable(
                name: "Periodo",
                newName: "Periodos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Periodos",
                table: "Periodos",
                column: "IdPeriodo");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Periodos_IdPeriodo",
                table: "Servicios",
                column: "IdPeriodo",
                principalTable: "Periodos",
                principalColumn: "IdPeriodo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Periodos_IdPeriodo",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Periodos",
                table: "Periodos");

            migrationBuilder.RenameTable(
                name: "Periodos",
                newName: "Periodo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Periodo",
                table: "Periodo",
                column: "IdPeriodo");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Periodo_IdPeriodo",
                table: "Servicios",
                column: "IdPeriodo",
                principalTable: "Periodo",
                principalColumn: "IdPeriodo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
