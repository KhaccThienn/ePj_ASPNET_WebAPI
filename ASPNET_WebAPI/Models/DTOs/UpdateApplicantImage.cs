using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPNET_WebAPI.Models.DTOs
{
    public class UpdateApplicantImage
    {
        public string? Applicant_Id { get; set; }

        public string Applicant_Name { get; set; }

        [EmailAddress]
        public string EmailId { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public bool Gender { get; set; }

        public string? Avatar { get; set; }

        public string? OldImage { get; set; }

        public string Descriptions { get; set; }

        public string? Experience { get; set; }

        public ApplicantStatus Status { get; set; } = ApplicantStatus.NOT_IN_PROCESS;

        public DateTime? Created_Date { get; set; }

        public DateTime? Updated_Date { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
