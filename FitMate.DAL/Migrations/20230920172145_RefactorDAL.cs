using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRecords_Foods_FoodID",
                table: "FoodRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans");

            migrationBuilder.RenameColumn(
                name: "FoodID",
                table: "FoodRecords",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodRecords_FoodID",
                table: "FoodRecords",
                newName: "IX_FoodRecords_FoodId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkoutPlans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "NutritionTargets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FoodRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRecords_Foods_FoodId",
                table: "FoodRecords",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRecords_Foods_FoodId",
                table: "FoodRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "FoodRecords",
                newName: "FoodID");

            migrationBuilder.RenameIndex(
                name: "IX_FoodRecords_FoodId",
                table: "FoodRecords",
                newName: "IX_FoodRecords_FoodID");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkoutPlans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "NutritionTargets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FoodRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRecords_Foods_FoodID",
                table: "FoodRecords",
                column: "FoodID",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutPlans_AspNetUsers_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
