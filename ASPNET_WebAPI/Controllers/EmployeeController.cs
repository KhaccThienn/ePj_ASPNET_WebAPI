using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_WebAPI.Models.Data;
using ASPNET_WebAPI.Models.Domains;
using ASPNET_WebAPI.Models.Status;
using ASPNET_WebAPI.Utils;
using ASPNET_WebAPI.Models.DTOs;
using AutoMapper;

namespace ASPNET_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IMapper _mapper;

        public EmployeeController(DataContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.Include(e => e.Vacancies).Include(e => e.Department).Include(x => x.Interviews).ToListAsync();
            return employees;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            if (_context.Employees == null)
            {
                return NotFound(new Status(404, "Entry Employee is Null", null));
            }
            var employee = await _context.Employees.Include(e => e.Vacancies).Include(e => e.Department).Include(x => x.Interviews).FirstOrDefaultAsync(x => x.Employee_Number == id);

            if (employee == null)
            {
                return NotFound(new Status(404, "Cannot Found Employee", null));
            }

            return employee;
        }

        // GET: api/Department/5
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllByName(string name)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            
            var employees = await _context.Employees.Include(e => e.Vacancies).Include(e => e.Department).Include(x => x.Interviews).Where(x => x.Employee_Name.Contains(name)).ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Employees.Include(e => e.Vacancies).Include(e => e.Department).Include(x => x.Interviews).ToListAsync();
            }
            return employees;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, [FromForm] UpdateEmployeeImage employee)
        {
            if (id != employee.Employee_Number)
            {
                return BadRequest(new Status(404, "Id does not Match", null));
            }
            if (_context.Employees.Any(x => x.EmailId == employee.EmailId && x.Employee_Number != employee.Employee_Number))
            {
                return BadRequest(new Status(400, "Email already in use", null));
            }

            if (employee.ImageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employee", employee.ImageFile.FileName);
                var oldFileName = employee.OldImage.Split($"{HttpContext.Request.Host.Value}/uploads/employee/");
                Console.WriteLine($"Emp old File: {oldFileName[1]}");
                var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employee", oldFileName[1]);
                if (System.IO.File.Exists(pathOldFile))
                {
                    System.IO.File.Delete(pathOldFile);
                }
                using (var file = System.IO.File.Create(path))
                {
                    await employee.ImageFile.CopyToAsync(file);
                }
                employee.Avatar = $"https://{HttpContext.Request.Host.Value}/uploads/employee/{employee.ImageFile.FileName}";
            }
            else
            {
                employee.Avatar = employee.OldImage;
            }

            employee.Updated_Date = DateTime.Now;
            var emp = _mapper.Map<Employee>(employee);
            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound(new Status(404, "Id Not Found", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(201, "Update Success", emp));
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] UpdateEmployeePassword employee)
        {
            if (id != employee.Employee_Number)
            {
                return BadRequest(new Status(404, "Id does not Match", null));
            }
            var empFound = await _context.Employees.FirstOrDefaultAsync(x => x.Employee_Number == id);
            if (empFound == null)
            {
                return Ok(new Status(404, "Not Found", employee));
            }
            if (employee.NewPassword != employee.ConfirmPassword)
            {
                return Ok(new Status(400, "Confirm Password Does Not Match", employee));
            }
            if (!PasswordBusiness.VerifyHashedPassword(empFound.Password, employee.OldPassword))
            {
                return Ok(new Status(400, "Password Does Not Match", employee));
            }
            if (!ModelState.IsValid)
            {
                return Ok(new Status(400, "Having Error When Update Password", ModelState.ToList()));
            }
            empFound.Password = PasswordBusiness.HashPassword(employee.NewPassword);
            empFound.Updated_Date = DateTime.Now;
            _context.Update(empFound);
            await _context.SaveChangesAsync();
            return Ok(new Status(201, "Update Success", empFound));
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromForm] EmployeeImage emp)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DataContext.Employees'  is null.");
            }
            var latest = await _context.Employees.OrderByDescending(x => x.Employee_Number).FirstOrDefaultAsync();
            int strnumber = 0;
            if (latest != null && string.IsNullOrEmpty(emp.Employee_Number))
            {
                strnumber = int.Parse(latest.DepartmentId.Substring(1));

                if (strnumber > 5000)
                {
                    return BadRequest(new Status(400, "Out Of Range Id", null));
                }
                emp.Employee_Number = IdGenerator.GenerateNextId(1, latest.Employee_Number);
            }
            else
            {
                if (!string.IsNullOrEmpty(emp.Employee_Number))
                {
                    strnumber = int.Parse(emp.DepartmentId.Substring(1));
                    if (strnumber > 5000)
                    {
                        return BadRequest(new Status(400, "Out Of Range Id", null));
                    }
                }

                emp.Employee_Number = IdGenerator.GenerateNextId(1, "E0000");
            }

            if (_context.Employees.Any(x => x.EmailId == emp.EmailId))
            {
                return BadRequest(new Status(400, "Email already in use", null));
            }
            if (emp.ImageFile == null)
            {
                return BadRequest(new Status(400, "Image Is Required", null));
            }

            if (emp.ImageFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employee", emp.ImageFile.FileName);
                using (var file = System.IO.File.Create(path))
                {
                    await emp.ImageFile.CopyToAsync(file);
                }
                emp.Avatar = $"https://{HttpContext.Request.Host.Value}/uploads/employee/{emp.ImageFile.FileName}";
            }

            emp.Password = PasswordBusiness.HashPassword(emp.Password);

            emp.Created_Date = DateTime.Now;

            var employee = _mapper.Map<Employee>(emp);

            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(emp.Employee_Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Status(200, "Created Success", CreatedAtAction("GetEmployee", new { id = employee.Employee_Number }, employee)));
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (_context.Employees == null)
            {
                return NotFound(new Status(404, "context Employee Is Null"));
            }
            var employee = await _context.Employees.Include(e => e.Vacancies).Include(e => e.Department).Include(x => x.Interviews).FirstOrDefaultAsync(x => x.Employee_Number == id);

            if (employee.Vacancies.Count > 0 || employee.Interviews.Count > 0)
            {
                return BadRequest(new Status(400, "Cannot Delete"));
            }

            var oldFileName = employee.Avatar.Split($"{HttpContext.Request.Host.Value}/uploads/employee/");
            Console.WriteLine($"Emp old File: {oldFileName[1]}");
            var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "employee", oldFileName[1]);
            if (System.IO.File.Exists(pathOldFile))
            {
                System.IO.File.Delete(pathOldFile);
            }
            if (employee == null)
            {
                return NotFound(new Status(404, "Not Found Id To Delete"));
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new Status(201, "Delete Success", employee));
        }

        private bool EmployeeExists(string id)
        {
            return (_context.Employees?.Any(e => e.Employee_Number == id)).GetValueOrDefault();
        }
    }
}
