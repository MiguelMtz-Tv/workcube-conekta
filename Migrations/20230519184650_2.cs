using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_ServicioTipos_IdServicioTipo",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioTipos",
                table: "ServicioTipos");

            migrationBuilder.RenameTable(
                name: "ServicioTipos",
                newName: "ServiciosTipos");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Servicios",
                newName: "IdServicioEstatus");

            migrationBuilder.RenameColumn(
                name: "PeriodoName",
                table: "Periodos",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ClienteRazonSocial",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodoName",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicioEstatusName",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicioTipoDescripcion",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicioTipoName",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosTipos",
                table: "ServiciosTipos",
                column: "IdServicioTipo");

            migrationBuilder.CreateTable(
                name: "ServiciosEstatus",
                columns: table => new
                {
                    IdServicioEstatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosEstatus", x => x.IdServicioEstatus);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdServicioEstatus",
                table: "Servicios",
                column: "IdServicioEstatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_ServiciosEstatus_IdServicioEstatus",
                table: "Servicios",
                column: "IdServicioEstatus",
                principalTable: "ServiciosEstatus",
                principalColumn: "IdServicioEstatus",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_ServiciosTipos_IdServicioTipo",
                table: "Servicios",
                column: "IdServicioTipo",
                principalTable: "ServiciosTipos",
                principalColumn: "IdServicioTipo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_ServiciosEstatus_IdServicioEstatus",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_ServiciosTipos_IdServicioTipo",
                table: "Servicios");

            migrationBuilder.DropTable(
                name: "ServiciosEstatus");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_IdServicioEstatus",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosTipos",
                table: "ServiciosTipos");

            migrationBuilder.DropColumn(
                name: "ClienteRazonSocial",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "PeriodoName",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ServicioEstatusName",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ServicioTipoDescripcion",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ServicioTipoName",
                table: "Servicios");

            migrationBuilder.RenameTable(
                name: "ServiciosTipos",
                newName: "ServicioTipos");

            migrationBuilder.RenameColumn(
                name: "IdServicioEstatus",
                table: "Servicios",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Periodos",
                newName: "PeriodoName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioTipos",
                table: "ServicioTipos",
                column: "IdServicioTipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_ServicioTipos_IdServicioTipo",
                table: "Servicios",
                column: "IdServicioTipo",
                principalTable: "ServicioTipos",
                principalColumn: "IdServicioTipo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
