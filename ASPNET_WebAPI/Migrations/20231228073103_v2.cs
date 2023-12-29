using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vacancy_Id",
                table: "Vacancy",
                newName: "Vacancy_Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vacancy_Number",
                table: "Vacancy",
                newName: "Vacancy_Id");
        }
    }
}
