using ASPNET_WebAPI.Models.Enums;

namespace ASPNET_WebAPI.Models.DTOs
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Roles? Role { get; set; }
    }
}
