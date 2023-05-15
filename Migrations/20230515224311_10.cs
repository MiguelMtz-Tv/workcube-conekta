using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_servicios_Clientes_IdCliente",
                table: "servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_servicios_Periodo_IdPeriodo",
                table: "servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_servicios_servicioTipos_IdServicioTipo",
                table: "servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_servicioTipos",
                table: "servicioTipos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_servicios",
                table: "servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cupon",
                table: "Cupon");

            migrationBuilder.RenameTable(
                name: "servicioTipos",
                newName: "ServicioTipos");

            migrationBuilder.RenameTable(
                name: "servicios",
                newName: "Servicios");

            migrationBuilder.RenameTable(
                name: "Cupon",
                newName: "Cupones");

            migrationBuilder.RenameIndex(
                name: "IX_servicios_IdServicioTipo",
                table: "Servicios",
                newName: "IX_Servicios_IdServicioTipo");

            migrationBuilder.RenameIndex(
                name: "IX_servicios_IdPeriodo",
                table: "Servicios",
                newName: "IX_Servicios_IdPeriodo");

            migrationBuilder.RenameIndex(
                name: "IX_servicios_IdCliente",
                table: "Servicios",
                newName: "IX_Servicios_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Cupon_IdServicio",
                table: "Cupones",
                newName: "IX_Cupones_IdServicio");

            migrationBuilder.RenameIndex(
                name: "IX_Cupon_IdCliente",
                table: "Cupones",
                newName: "IX_Cupones_IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioTipos",
                table: "ServicioTipos",
                column: "IdServicioTipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios",
                column: "IdServicio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cupones",
                table: "Cupones",
                column: "IdCupon");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Clientes_IdCliente",
                table: "Servicios",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Periodo_IdPeriodo",
                table: "Servicios",
                column: "IdPeriodo",
                principalTable: "Periodo",
                principalColumn: "IdPeriodo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_ServicioTipos_IdServicioTipo",
                table: "Servicios",
                column: "IdServicioTipo",
                principalTable: "ServicioTipos",
                principalColumn: "IdServicioTipo",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Clientes_IdCliente",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Periodo_IdPeriodo",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_ServicioTipos_IdServicioTipo",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioTipos",
                table: "ServicioTipos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicios",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cupones",
                table: "Cupones");

            migrationBuilder.RenameTable(
                name: "ServicioTipos",
                newName: "servicioTipos");

            migrationBuilder.RenameTable(
                name: "Servicios",
                newName: "servicios");

            migrationBuilder.RenameTable(
                name: "Cupones",
                newName: "Cupon");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_IdServicioTipo",
                table: "servicios",
                newName: "IX_servicios_IdServicioTipo");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_IdPeriodo",
                table: "servicios",
                newName: "IX_servicios_IdPeriodo");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_IdCliente",
                table: "servicios",
                newName: "IX_servicios_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Cupones_IdServicio",
                table: "Cupon",
                newName: "IX_Cupon_IdServicio");

            migrationBuilder.RenameIndex(
                name: "IX_Cupones_IdCliente",
                table: "Cupon",
                newName: "IX_Cupon_IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_servicioTipos",
                table: "servicioTipos",
                column: "IdServicioTipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_servicios",
                table: "servicios",
                column: "IdServicio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cupon",
                table: "Cupon",
                column: "IdCupon");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_Clientes_IdCliente",
                table: "Cupon",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Cupon_servicios_IdServicio",
                table: "Cupon",
                column: "IdServicio",
                principalTable: "servicios",
                principalColumn: "IdServicio");

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_Clientes_IdCliente",
                table: "servicios",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_Periodo_IdPeriodo",
                table: "servicios",
                column: "IdPeriodo",
                principalTable: "Periodo",
                principalColumn: "IdPeriodo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_servicios_servicioTipos_IdServicioTipo",
                table: "servicios",
                column: "IdServicioTipo",
                principalTable: "servicioTipos",
                principalColumn: "IdServicioTipo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
