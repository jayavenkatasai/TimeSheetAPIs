using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TIMESHEETAPI.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModels_registerations_registerationEmployeeID",
                table: "TaskModels");

            migrationBuilder.DropIndex(
                name: "IX_TaskModels_registerationEmployeeID",
                table: "TaskModels");

            migrationBuilder.DropColumn(
                name: "registerationEmployeeID",
                table: "TaskModels");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_EmployeeID",
                table: "TaskModels",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModels_registerations_EmployeeID",
                table: "TaskModels",
                column: "EmployeeID",
                principalTable: "registerations",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModels_registerations_EmployeeID",
                table: "TaskModels");

            migrationBuilder.DropIndex(
                name: "IX_TaskModels_EmployeeID",
                table: "TaskModels");

            migrationBuilder.AddColumn<int>(
                name: "registerationEmployeeID",
                table: "TaskModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_registerationEmployeeID",
                table: "TaskModels",
                column: "registerationEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModels_registerations_registerationEmployeeID",
                table: "TaskModels",
                column: "registerationEmployeeID",
                principalTable: "registerations",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
