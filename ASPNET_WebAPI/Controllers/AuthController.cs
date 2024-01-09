using ASPNET_WebAPI.Models.Data;
using ASPNET_WebAPI.Models.Domains;
using ASPNET_WebAPI.Models.DTOs;
using ASPNET_WebAPI.Models.Status;
using ASPNET_WebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNET_WebAPI.Controllers
{
    [EnableCors("AllowOrigins")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var hashedPass = PasswordBusiness.HashPassword(model.Password);
            var userFound = await _context.Employees.FirstOrDefaultAsync(x => x.Username == model.Username && x.Role == model.Role);

            if (userFound == null)
            {
                return BadRequest(new Status(400, "Login Failed"));
            }

            if (!PasswordBusiness.VerifyHashedPassword(userFound.Password, model.Password))
            {
                return BadRequest(new Status(400, "Password Does Not Match"));
            }

            // lay key tu appconfig
            var key = _configuration["Jwt:Key"];

            // encode key
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // sign vao key encoded
            var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // tao claims (optional)
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Role, userFound.Role.ToString()),
                    new Claim(ClaimTypes.Name, userFound.Employee_Name),
                };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signingCredential,
                claims: claims
                );

            // Generate token from JwtSecurityToken
            var tokenn = new JwtSecurityTokenHandler().WriteToken(token);

            // Return to client userData and token

            return new JsonResult(new Status(200, "Login Success", new
            {
                userData = userFound,
                token = tokenn
            }));
        }

        [HttpGet("getClaim")]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = from c in User.Claims
                         select new
                         {
                             Type = c.Type.Substring(c.Type.LastIndexOf("/") + 1),
                             c.Value
                         };
            return new JsonResult(claims);
        }
    }
}
