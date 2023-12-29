using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public string? DepartmentId { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [Column]
        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; } = DateTime.Now;

        [Column]
        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }

        public ICollection<Vacancy>? Vacancies { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
