using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Applicant_Vacancy")]
    public class Applicant_Vacancy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ApplicantId { get; set; }
        [ForeignKey("ApplicantId")]
        public Applicant? Applicant { get; set; }

        public string VacancyId { get; set; }
        [ForeignKey("VacancyId")]
        public Vacancy? Vacancy { get; set; }

        [Column]
        [DefaultValue("getdate()")]
        public DateTime DateAttached { get; set; } = DateTime.Now;
        
        [Column]
        public ApplicantVacancyStatus Status { get; set; }

        public ICollection<Interview>? Interviews { get; set; }
    }
}
