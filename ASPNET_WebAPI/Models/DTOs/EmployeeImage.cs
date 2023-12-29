using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPNET_WebAPI.Models.DTOs
{
    public class EmployeeImage
    {
        public string? Employee_Number { get; set; }

        public string Employee_Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [StringLength(25)]
        [EmailAddress]
        public string EmailId { get; set; }

        public string? Avatar { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; }

        public Roles Role { get; set; }

        public string DepartmentId { get; set; }

        public DateTime? Created_Date { get; set; } = DateTime.Now;

        public DateTime? Updated_Date { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
