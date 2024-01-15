using ASPNET_WebAPI.Models.Domains;
using ASPNET_WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebAPI.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ...
            modelBuilder.Entity<Department>()
                .HasData(
                    new Department { DepartmentId = "D0001", Name = "System", Created_Date = DateTime.Now },
                    new Department { DepartmentId = "D0002", Name = "HRD", Created_Date = DateTime.Now },
                    new Department { DepartmentId = "D0003", Name = "Developement", Created_Date = DateTime.Now }
                );
            modelBuilder.Entity<Employee>()
                .HasData(
                    new Employee
                    {
                        Employee_Number = "E0001",
                        Employee_Name = "Employee001",
                        Username = "emp001",
                        Password = "ABsSNuMHMKcKABH2f99w4+ykLJhZMU81jr/6kC6wKXxXF1MReEAv/DFf5msEhbIpDw==",
                        Address = "Hanoi",
                        Avatar = "https://localhost:7144/uploads/employee/default.png",
                        EmailId = "muzan01@gmail.com",
                        Gender = true,
                        Role = Enums.Roles.ADMIN,
                        DepartmentId = "D0001",
                        Created_Date = DateTime.Now
                    },
                    new Employee
                    {
                        Employee_Number = "E0002",
                        Employee_Name = "Employee002",
                        Username = "emp002",
                        Password = "ABsSNuMHMKcKABH2f99w4+ykLJhZMU81jr/6kC6wKXxXF1MReEAv/DFf5msEhbIpDw==",
                        Address = "Hanoi",
                        Avatar = "https://localhost:7144/uploads/employee/default.png",
                        EmailId = "muzan02@gmail.com",
                        Gender = true,
                        Role = Enums.Roles.HR,
                        DepartmentId = "D0002",
                        Created_Date = DateTime.Now
                    },
                    new Employee
                    {
                        Employee_Number = "E0003",
                        Employee_Name = "Employee003",
                        Username = "emp003",
                        Password = "ABsSNuMHMKcKABH2f99w4+ykLJhZMU81jr/6kC6wKXxXF1MReEAv/DFf5msEhbIpDw==",
                        Address = "Hanoi",
                        Avatar = "https://localhost:7144/uploads/employee/default.png",
                        EmailId = "muzan03@gmail.com",
                        Gender = false,
                        Role = Enums.Roles.INTERVIEW,
                        DepartmentId = "D0003",
                        Created_Date = DateTime.Now
                    }
                );
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        // DBSets
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Applicant_Vacancy> Applicant_Vacancy { get; set; }
        public DbSet<Interview> Interview { get; set; }
    }
}
