using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Migrations
{
    /// <inheritdoc />
    public partial class RenameIDtoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalID",
                table: "GoalProgressRecords");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "WorkoutPlans",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserFoods",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "NutritionTargets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Goals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GoalID",
                table: "GoalProgressRecords",
                newName: "GoalId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "GoalProgressRecords",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_GoalProgressRecords_GoalID",
                table: "GoalProgressRecords",
                newName: "IX_GoalProgressRecords_GoalId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "FoodRecords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BodyweightTargets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BodyweightRecords",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Activity",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GoalId",
                table: "GoalProgressRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalId",
                table: "GoalProgressRecords",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalId",
                table: "GoalProgressRecords");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WorkoutPlans",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserFoods",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NutritionTargets",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Goals",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "GoalId",
                table: "GoalProgressRecords",
                newName: "GoalID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GoalProgressRecords",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_GoalProgressRecords_GoalId",
                table: "GoalProgressRecords",
                newName: "IX_GoalProgressRecords_GoalID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FoodRecords",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BodyweightTargets",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BodyweightRecords",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "Activity",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "GoalID",
                table: "GoalProgressRecords",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalID",
                table: "GoalProgressRecords",
                column: "GoalID",
                principalTable: "Goals",
                principalColumn: "ID");
        }
    }
}
