using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Vacancy")]
    public class Vacancy
    {
        [Key]
        public string? Vacancy_Number { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Vacancy_Title { get; set; }

        [Column]
        [DefaultValue("getdate()")]
        public DateTime Date_Created { get; set; } = DateTime.Now;

        public string OwnedById { get; set; }
        [ForeignKey("OwnedById")]
        public Employee? OwnedBy { get; set; }

        [Column]
        public VacancyStatus Status { get; set; } = VacancyStatus.OPEN;

        [Column]
        public int NumberOfJobs { get; set; }

        [Column(TypeName = "ntext")]
        public string Descriptions { get; set; }

        public string DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; } = DateTime.Now;

        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }

        [DefaultValue("getdate()")]
        public DateTime? Closed_Date { get; set; }

        public ICollection<Applicant_Vacancy>? Applicant_Vacancy { get; set; }
    }
}
