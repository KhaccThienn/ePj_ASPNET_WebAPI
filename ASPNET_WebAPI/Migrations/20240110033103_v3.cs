using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Vacancy",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Vacancy",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequiredSkill",
                table: "Vacancy",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "RequiredSkill",
                table: "Vacancy");
        }
    }
}
