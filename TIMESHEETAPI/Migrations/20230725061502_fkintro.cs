using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TIMESHEETAPI.Migrations
{
    /// <inheritdoc />
    public partial class fkintro : Migration
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
                name: "NewRegisterationEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verifiedat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordresetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordTokenExpires = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewRegisterationEmployees", x => x.Id);
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
                name: "registerations",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registerations", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "superHeroes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_superHeroes", x => x.id);
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
                    ProjectID = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_TaskModels_ProjectModels_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "ProjectModels",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModels_registerations_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "registerations",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOauths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOauths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOauths_registerations_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "registerations",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_ActivityID",
                table: "TaskModels",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_EmployeeID",
                table: "TaskModels",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModels_ProjectID",
                table: "TaskModels",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UserOauths_EmployeeID",
                table: "UserOauths",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewRegisterationEmployees");

            migrationBuilder.DropTable(
                name: "superHeroes");

            migrationBuilder.DropTable(
                name: "TaskModels");

            migrationBuilder.DropTable(
                name: "UserOauths");

            migrationBuilder.DropTable(
                name: "ActivityModels");

            migrationBuilder.DropTable(
                name: "ProjectModels");

            migrationBuilder.DropTable(
                name: "registerations");
        }
    }
}
