﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workcube_pagos.Migrations
{
    /// <inheritdoc />
    public partial class settingMailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuario");
        }
    }
}
