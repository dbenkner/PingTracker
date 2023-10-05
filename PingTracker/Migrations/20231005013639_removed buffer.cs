using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class removedbuffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buffer",
                table: "PingResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buffer",
                table: "PingResults",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
