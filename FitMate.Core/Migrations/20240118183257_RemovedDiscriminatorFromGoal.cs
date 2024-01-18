using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Core.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDiscriminatorFromGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Goals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
