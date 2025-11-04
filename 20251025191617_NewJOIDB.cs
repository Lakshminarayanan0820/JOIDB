using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JOIEnergy.Migrations
{
    /// <inheritdoc />
    public partial class NewJOIDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnergySupplier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "SmartMeters",
                columns: table => new
                {
                    SmartMeterId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartMeters", x => x.SmartMeterId);
                    table.ForeignKey(
                        name: "FK_SmartMeters_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    ReadingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmartMeterId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.ReadingId);
                    table.ForeignKey(
                        name: "FK_Readings_SmartMeters_SmartMeterId",
                        column: x => x.SmartMeterId,
                        principalTable: "SmartMeters",
                        principalColumn: "SmartMeterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_SmartMeterId",
                table: "Readings",
                column: "SmartMeterId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartMeters_PlanId",
                table: "SmartMeters",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "SmartMeters");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
