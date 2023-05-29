using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroContrato",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerID",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeCustomerID",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "NumeroContrato",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
