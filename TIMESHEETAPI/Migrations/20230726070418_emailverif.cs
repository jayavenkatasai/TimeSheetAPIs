using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TIMESHEETAPI.Migrations
{
    /// <inheritdoc />
    public partial class emailverif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "registerations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "registerations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "registerations");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "registerations");
        }
    }
}
