using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNET_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Applicant_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Applicant_Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(225)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Applicant_Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Employee_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Employee_Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false),
                    EmailId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Avatar = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Employee_Number);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Vacancy_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Vacancy_Title = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NumberOfJobs = table.Column<int>(type: "int", nullable: false),
                    RequiredSkill = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Descriptions = table.Column<string>(type: "ntext", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Closed_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Vacancy_Number);
                    table.ForeignKey(
                        name: "FK_Vacancy_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacancy_Employee_OwnedById",
                        column: x => x.OwnedById,
                        principalTable: "Employee",
                        principalColumn: "Employee_Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applicant_Vacancy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VacancyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAttached = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant_Vacancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicant_Vacancy_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "Applicant_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applicant_Vacancy_Vacancy_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancy",
                        principalColumn: "Vacancy_Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    InterviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Vacancy_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Applicant_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "ntext", nullable: true),
                    InterviewStatuss = table.Column<int>(type: "int", nullable: true),
                    Applicant_Vacancy_Id = table.Column<int>(type: "int", nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.InterviewId);
                    table.ForeignKey(
                        name: "FK_Interview_Applicant_Applicant_Id",
                        column: x => x.Applicant_Id,
                        principalTable: "Applicant",
                        principalColumn: "Applicant_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interview_Applicant_Vacancy_Applicant_Vacancy_Id",
                        column: x => x.Applicant_Vacancy_Id,
                        principalTable: "Applicant_Vacancy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interview_Employee_EmployeeNumber",
                        column: x => x.EmployeeNumber,
                        principalTable: "Employee",
                        principalColumn: "Employee_Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interview_Vacancy_Vacancy_Number",
                        column: x => x.Vacancy_Number,
                        principalTable: "Vacancy",
                        principalColumn: "Vacancy_Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_Vacancy_ApplicantId",
                table: "Applicant_Vacancy",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_Vacancy_VacancyId",
                table: "Applicant_Vacancy",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Applicant_Id",
                table: "Interview",
                column: "Applicant_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Applicant_Vacancy_Id",
                table: "Interview",
                column: "Applicant_Vacancy_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_EmployeeNumber",
                table: "Interview",
                column: "EmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Vacancy_Number",
                table: "Interview",
                column: "Vacancy_Number");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_DepartmentId",
                table: "Vacancy",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_OwnedById",
                table: "Vacancy",
                column: "OwnedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.DropTable(
                name: "Applicant_Vacancy");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "Vacancy");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
