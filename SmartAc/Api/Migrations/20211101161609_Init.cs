using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Major = table.Column<int>(type: "int", nullable: false),
                    Minor = table.Column<int>(type: "int", nullable: false),
                    Patch = table.Column<int>(type: "int", nullable: false),
                    InitedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measures",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecordedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Carbon = table.Column<double>(type: "float", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => new { x.DeviceId, x.RecordedOn });
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RecognizedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasureDeviceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MeasureRecordedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    View = table.Column<int>(type: "int", nullable: false),
                    Resolution = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Measures_MeasureDeviceId_MeasureRecordedOn",
                        columns: x => new { x.MeasureDeviceId, x.MeasureRecordedOn },
                        principalTable: "Measures",
                        principalColumns: new[] { "DeviceId", "RecordedOn" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_MeasureDeviceId_MeasureRecordedOn",
                table: "Alerts",
                columns: new[] { "MeasureDeviceId", "MeasureRecordedOn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Measures");
        }
    }
}
