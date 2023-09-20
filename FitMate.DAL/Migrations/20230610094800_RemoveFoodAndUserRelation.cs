using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitMate.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFoodAndUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRecords_UserFoods_FoodID",
                table: "FoodRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFoods_AspNetUsers_CreatedByID",
                table: "UserFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFoods",
                table: "UserFoods");

            migrationBuilder.DropIndex(
                name: "IX_UserFoods_CreatedByID",
                table: "UserFoods");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "UserFoods");

            migrationBuilder.RenameTable(
                name: "UserFoods",
                newName: "Foods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRecords_Foods_FoodID",
                table: "FoodRecords",
                column: "FoodID",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodRecords_Foods_FoodID",
                table: "FoodRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "UserFoods");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByID",
                table: "UserFoods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFoods",
                table: "UserFoods",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoods_CreatedByID",
                table: "UserFoods",
                column: "CreatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodRecords_UserFoods_FoodID",
                table: "FoodRecords",
                column: "FoodID",
                principalTable: "UserFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFoods_AspNetUsers_CreatedByID",
                table: "UserFoods",
                column: "CreatedByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
