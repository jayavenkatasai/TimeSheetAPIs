using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TIMESHEETAPI.Migrations
{
    /// <inheritdoc />
    public partial class loginverification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOtpVerified",
                table: "registerations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtpVerificationToken",
                table: "registerations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOtpVerified",
                table: "registerations");

            migrationBuilder.DropColumn(
                name: "OtpVerificationToken",
                table: "registerations");
        }
    }
}
