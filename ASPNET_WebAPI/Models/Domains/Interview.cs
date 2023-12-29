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
        public DateTime InterviewDate { get; set; } = DateTime.Now;

        [Column]
        [DefaultValue("getdate()")]
        public DateTime DateStarted { get; set; } = DateTime.Now;

        [Column]
        [DefaultValue("getdate()")]
        public DateTime DateEnd { get; set; } = DateTime.Now;

        public string EmployeeNumber { get; set; }
        [ForeignKey("EmployeeNumber")]
        public Employee? Employee { get; set; }

        [Column]
        public InterviewStatus InterviewStatus { get; set; }

        public int Applicant_Vacancy_Id { get; set; }
        [ForeignKey("Applicant_Vacancy_Id")]
        public Applicant_Vacancy? Applicant_Vacancy { get; set; }

        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; } = DateTime.Now;

        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }
    }
}
