using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class pingresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PingResultId",
                table: "Websites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PingResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RTT = table.Column<int>(type: "int", nullable: false),
                    Buffer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WebsiteId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PingResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Websites_PingResultId",
                table: "Websites",
                column: "PingResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_PingResults_PingResultId",
                table: "Websites",
                column: "PingResultId",
                principalTable: "PingResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Websites_PingResults_PingResultId",
                table: "Websites");

            migrationBuilder.DropTable(
                name: "PingResults");

            migrationBuilder.DropIndex(
                name: "IX_Websites_PingResultId",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "PingResultId",
                table: "Websites");
        }
    }
}
