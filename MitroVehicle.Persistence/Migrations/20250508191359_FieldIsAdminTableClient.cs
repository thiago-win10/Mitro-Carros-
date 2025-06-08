using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitroVehicle.Persistence.Migrations
{
    public partial class FieldIsAdminTableClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Clients");
        }
    }
}
