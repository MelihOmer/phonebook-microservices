using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    requested_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    report_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    report_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    person_count = table.Column<int>(type: "integer", nullable: false),
                    phone_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_details_reports_report_id",
                        column: x => x.report_id,
                        principalTable: "reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_report_details_report_id",
                table: "report_details",
                column: "report_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "report_details");

            migrationBuilder.DropTable(
                name: "reports");
        }
    }
}
