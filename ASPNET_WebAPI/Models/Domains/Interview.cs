using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Interview")]
    public class Interview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterviewId { get; set; }

        [Column]
        [DefaultValue("getdate()")]
        public DateTime InterviewDate { get; set; }

        [Column]
        [DefaultValue("getdate()")]
        public string? DateStarted { get; set; } 

        [Column]
        [DefaultValue("getdate()")]
        public string? DateEnd { get; set; }

        public string EmployeeNumber { get; set; }
        [ForeignKey("EmployeeNumber")]
        public Employee? Employee { get; set; }

        public string Vacancy_Number { get; set; }
        [ForeignKey("Vacancy_Number")]
        public Vacancy? Vacancy { get; set; }

        public string Applicant_Id { get; set; }
        [ForeignKey("Applicant_Id")]
        public Applicant? Applicant { get; set; }

        [Column(TypeName = "ntext")]
        public string? Note { get; set; }

        [Column]
        public InterviewStatus? InterviewStatuss { get; set; }

        public int Applicant_Vacancy_Id { get; set; }
        [ForeignKey("Applicant_Vacancy_Id")]
        public Applicant_Vacancy? Applicant_Vacancy { get; set; }

        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; }

        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }
    }
}
