using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public string? Employee_Number { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Employee_Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }

        [StringLength(25)]
        [Column(TypeName = "varchar(25)")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? Avatar { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        [Column]
        public bool Gender { get; set; }

        [Column]
        public Roles Role { get; set; }

        // Navigator Attribute
        public string DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public ICollection<Vacancy>? Vacancies { get; set; }
        public ICollection<Interview>? Interviews { get; set; }


        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; } = DateTime.Now;

        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }
    }
}
