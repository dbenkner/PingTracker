using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class traceroute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraceResults",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateTimeOfTrace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    websiteId = table.Column<int>(type: "int", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraceResults", x => x.id);
                    table.ForeignKey(
                        name: "FK_TraceResults_Websites_websiteId",
                        column: x => x.websiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraceLines",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ping1 = table.Column<long>(type: "bigint", nullable: true),
                    ping2 = table.Column<long>(type: "bigint", nullable: true),
                    ping3 = table.Column<long>(type: "bigint", nullable: true),
                    hop = table.Column<int>(type: "int", nullable: false),
                    traceResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraceLines", x => x.id);
                    table.ForeignKey(
                        name: "FK_TraceLines_TraceResults_traceResultId",
                        column: x => x.traceResultId,
                        principalTable: "TraceResults",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraceLines_traceResultId",
                table: "TraceLines",
                column: "traceResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TraceResults_websiteId",
                table: "TraceResults",
                column: "websiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraceLines");

            migrationBuilder.DropTable(
                name: "TraceResults");
        }
    }
}
