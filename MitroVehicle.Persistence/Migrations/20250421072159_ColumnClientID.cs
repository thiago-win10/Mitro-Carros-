using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitroVehicle.Persistence.Migrations
{
    public partial class ColumnClientID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vehicle");
        }
    }
}
