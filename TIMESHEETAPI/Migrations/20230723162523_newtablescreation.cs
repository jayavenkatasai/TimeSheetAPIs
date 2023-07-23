using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TIMESHEETAPI.Migrations
{
    /// <inheritdoc />
    public partial class newtablescreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityModels",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityModels", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectModels",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModels", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "TaskModels",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ActivityID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    ProjectModelProjectId = table.Column<int>(type: "int", nullable: false),
                    registerationEmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModels", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskModels_ActivityModels_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "ActivityModels",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModels_ProjectModels_ProjectModelProjectId",
                        column: x => x.ProjectModelProjectId,
                        principalTable: "ProjectModels",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModels_registerations_registerationEmployeeID",
                        column: x => x.registerationEmployeeID,
                        principalTable: "registerations",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_ActivityID",
                table: "TaskModels",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_ProjectModelProjectId",
                table: "TaskModels",
                column: "ProjectModelProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_registerationEmployeeID",
                table: "TaskModels",
                column: "registerationEmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModels");

            migrationBuilder.DropTable(
                name: "ActivityModels");

            migrationBuilder.DropTable(
                name: "ProjectModels");
        }
    }
}
