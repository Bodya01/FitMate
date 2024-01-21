using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Core.Migrations
{
    /// <inheritdoc />
    public partial class MovedGoalsToTPTApproach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "QuantityUnit",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Goals");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TimedGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    QuantityUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedGoal_Goals_Id",
                        column: x => x.Id,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightliftingGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightliftingGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightliftingGoal_Goals_Id",
                        column: x => x.Id,
                        principalTable: "Goals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimedGoal");

            migrationBuilder.DropTable(
                name: "WeightliftingGoal");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Goals",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuantityUnit",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "Goals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Goals",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Goals",
                type: "real",
                nullable: true);
        }
    }
}
