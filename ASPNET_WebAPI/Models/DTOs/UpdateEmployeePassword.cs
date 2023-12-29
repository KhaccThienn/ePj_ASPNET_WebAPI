using ASPNET_WebAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebAPI.Models.DTOs
{
    public class UpdateEmployeePassword
    {
        public string Employee_Number { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
