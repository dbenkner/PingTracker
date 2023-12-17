using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class userid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Websites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Websites_UserId",
                table: "Websites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_Websites_UserId",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Websites");
        }
    }
}
