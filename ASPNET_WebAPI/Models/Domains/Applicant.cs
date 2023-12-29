using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Domains
{
    [Table("Applicant")]
    public class Applicant
    {
        [Key]
        public string? Applicant_Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Applicant_Name { get; set; }

        [Column(TypeName = "varchar(25)")]
        [StringLength(25)]
        [EmailAddress]
        public string EmailId { get; set; }

        [Column(TypeName = "nvarchar(225)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PhoneNumber { get; set; }

        [Column]
        public bool Gender { get; set; }

        [Column]
        public string? Avatar { get; set; }

        [Column]
        public string Descriptions { get; set; }

        [Column]
        public string? Experience { get; set; }

        [Column]
        public ApplicantStatus Status { get; set; } = ApplicantStatus.NOT_IN_PROCESS;

        [Column]
        [DefaultValue("getdate()")]
        public DateTime? Created_Date { get; set; } = DateTime.Now;

        [Column]
        [DefaultValue("getdate()")]
        public DateTime? Updated_Date { get; set; }

        public ICollection<Applicant_Vacancy>? Applicant_Vacancy { get; set; }
    }
}
