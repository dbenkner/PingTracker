using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingTracker.Migrations
{
    /// <inheritdoc />
    public partial class addedwebsitenicknamecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebsiteNickname",
                table: "Websites",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteNickname",
                table: "Websites");
        }
    }
}
