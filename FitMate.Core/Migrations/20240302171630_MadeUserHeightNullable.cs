using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Core.Migrations
{
    /// <inheritdoc />
    public partial class MadeUserHeightNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "AspNetUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");
        }
    }
}
