using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class RenamedDeviceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Latest",
                table: "Devices",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "Initial",
                table: "Devices",
                newName: "InitedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Devices",
                newName: "Latest");

            migrationBuilder.RenameColumn(
                name: "InitedOn",
                table: "Devices",
                newName: "Initial");
        }
    }
}
