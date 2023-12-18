using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class pingresultsuserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PingResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PingResults_UserId",
                table: "PingResults",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_Users_UserId",
                table: "PingResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_Users_UserId",
                table: "PingResults");

            migrationBuilder.DropIndex(
                name: "IX_PingResults_UserId",
                table: "PingResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PingResults");
        }
    }
}
