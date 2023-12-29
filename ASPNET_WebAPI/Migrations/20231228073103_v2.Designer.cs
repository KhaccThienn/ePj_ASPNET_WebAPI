﻿// <auto-generated />
using System;
using ASPNET_WebAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPNET_WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231228073103_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Applicant", b =>
                {
                    b.Property<string>("Applicant_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("Applicant_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Experience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Applicant_Id");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Applicant_Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateAttached")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VacancyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Applicant_Vacancy");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("Updated_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Employee", b =>
                {
                    b.Property<string>("Employee_Number")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Employee_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Employee_Number");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Interview", b =>
                {
                    b.Property<int>("InterviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterviewId"));

                    b.Property<int>("Applicant_Vacancy_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("InterviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InterviewStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("InterviewId");

                    b.HasIndex("Applicant_Vacancy_Id");

                    b.HasIndex("EmployeeNumber");

                    b.ToTable("Interview");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Vacancy", b =>
                {
                    b.Property<string>("Vacancy_Number")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Closed_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descriptions")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<int>("NumberOfJobs")
                        .HasColumnType("int");

                    b.Property<string>("OwnedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Vacancy_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Vacancy_Number");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("OwnedById");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Applicant_Vacancy", b =>
                {
                    b.HasOne("ASPNET_WebAPI.Models.Domains.Applicant", "Applicant")
                        .WithMany("Applicant_Vacancy")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ASPNET_WebAPI.Models.Domains.Vacancy", "Vacancy")
                        .WithMany("Applicant_Vacancy")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Employee", b =>
                {
                    b.HasOne("ASPNET_WebAPI.Models.Domains.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Interview", b =>
                {
                    b.HasOne("ASPNET_WebAPI.Models.Domains.Applicant_Vacancy", "Applicant_Vacancy")
                        .WithMany("Interviews")
                        .HasForeignKey("Applicant_Vacancy_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ASPNET_WebAPI.Models.Domains.Employee", "Employee")
                        .WithMany("Interviews")
                        .HasForeignKey("EmployeeNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Applicant_Vacancy");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Vacancy", b =>
                {
                    b.HasOne("ASPNET_WebAPI.Models.Domains.Department", "Department")
                        .WithMany("Vacancies")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ASPNET_WebAPI.Models.Domains.Employee", "OwnedBy")
                        .WithMany("Vacancies")
                        .HasForeignKey("OwnedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("OwnedBy");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Applicant", b =>
                {
                    b.Navigation("Applicant_Vacancy");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Applicant_Vacancy", b =>
                {
                    b.Navigation("Interviews");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Employee", b =>
                {
                    b.Navigation("Interviews");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("ASPNET_WebAPI.Models.Domains.Vacancy", b =>
                {
                    b.Navigation("Applicant_Vacancy");
                });
#pragma warning restore 612, 618
        }
    }
}
