using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourFitnessTracker.Core.Migrations
{
    /// <inheritdoc />
    public partial class MovedGoalsToSeparateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalProgressRecords_AspNetUsers_UserId",
                table: "GoalProgressRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalId",
                table: "GoalProgressRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TimedGoal_Goals_Id",
                table: "TimedGoal");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingGoal_Goals_Id",
                table: "WeightliftingGoal");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalProgressRecords",
                table: "GoalProgressRecords");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "GoalProgressRecords");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GoalProgressRecords");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "GoalProgressRecords");

            migrationBuilder.RenameTable(
                name: "GoalProgressRecords",
                newName: "WeightliftingProgressRecords");

            migrationBuilder.RenameIndex(
                name: "IX_GoalProgressRecords_UserId",
                table: "WeightliftingProgressRecords",
                newName: "IX_WeightliftingProgressRecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalProgressRecords_GoalId",
                table: "WeightliftingProgressRecords",
                newName: "IX_WeightliftingProgressRecords_GoalId");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "WeightliftingGoal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WeightliftingGoal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "TimedGoal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TimedGoal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "WeightliftingProgressRecords",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "WeightliftingProgressRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightliftingProgressRecords",
                table: "WeightliftingProgressRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TimedProgressRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    GoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedProgressRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedProgressRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimedProgressRecords_TimedGoal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "TimedGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeightliftingGoal_UserId",
                table: "WeightliftingGoal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedGoal_UserId",
                table: "TimedGoal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedProgressRecords_GoalId",
                table: "TimedProgressRecords",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedProgressRecords_UserId",
                table: "TimedProgressRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimedGoal_AspNetUsers_UserId",
                table: "TimedGoal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingGoal_AspNetUsers_UserId",
                table: "WeightliftingGoal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingProgressRecords_AspNetUsers_UserId",
                table: "WeightliftingProgressRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingProgressRecords_WeightliftingGoal_GoalId",
                table: "WeightliftingProgressRecords",
                column: "GoalId",
                principalTable: "WeightliftingGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimedGoal_AspNetUsers_UserId",
                table: "TimedGoal");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingGoal_AspNetUsers_UserId",
                table: "WeightliftingGoal");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingProgressRecords_AspNetUsers_UserId",
                table: "WeightliftingProgressRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightliftingProgressRecords_WeightliftingGoal_GoalId",
                table: "WeightliftingProgressRecords");

            migrationBuilder.DropTable(
                name: "TimedProgressRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeightliftingGoal_UserId",
                table: "WeightliftingGoal");

            migrationBuilder.DropIndex(
                name: "IX_TimedGoal_UserId",
                table: "TimedGoal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightliftingProgressRecords",
                table: "WeightliftingProgressRecords");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "WeightliftingGoal");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WeightliftingGoal");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "TimedGoal");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimedGoal");

            migrationBuilder.RenameTable(
                name: "WeightliftingProgressRecords",
                newName: "GoalProgressRecords");

            migrationBuilder.RenameIndex(
                name: "IX_WeightliftingProgressRecords_UserId",
                table: "GoalProgressRecords",
                newName: "IX_GoalProgressRecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WeightliftingProgressRecords_GoalId",
                table: "GoalProgressRecords",
                newName: "IX_GoalProgressRecords_GoalId");

            migrationBuilder.AlterColumn<float>(
                name: "Weight",
                table: "GoalProgressRecords",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "GoalProgressRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "GoalProgressRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "GoalProgressRecords",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "GoalProgressRecords",
                type: "time",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalProgressRecords",
                table: "GoalProgressRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalProgressRecords_AspNetUsers_UserId",
                table: "GoalProgressRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalProgressRecords_Goals_GoalId",
                table: "GoalProgressRecords",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimedGoal_Goals_Id",
                table: "TimedGoal",
                column: "Id",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightliftingGoal_Goals_Id",
                table: "WeightliftingGoal",
                column: "Id",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
