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
using Microsoft.AspNetCore.Authorization;
using ASPNET_WebAPI.Models.Enums;

namespace ASPNET_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            return await _context.Departments.Include(d => d.Vacancies).Include(d => d.Employees).ToListAsync();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(string id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.Include(d => d.Vacancies).Include(d => d.Employees).FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> PutDepartment(string id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest(new Status(404, "Id does not Match", null));
            }
            department.Updated_Date = DateTime.Now;
            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound(new Status(404, "Id Not Found", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(201, "Update Success", department));
        }

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'DataContext.Departments'  is null.");
            }


            var latest = await _context.Departments.OrderByDescending(x => x.DepartmentId).FirstOrDefaultAsync();
            int strnumber = 0;
            if (latest != null && string.IsNullOrEmpty(department.DepartmentId))
            {
                strnumber = int.Parse(latest.DepartmentId.Substring(1));

                if (strnumber > 5000)
                {
                    return BadRequest(new Status(400, "Out Of Range Id", null));
                }
                department.DepartmentId = IdGenerator.GenerateNextId(1, latest.DepartmentId);
            }
            else
            {
                if (!string.IsNullOrEmpty(department.DepartmentId))
                {
                    strnumber = int.Parse(department.DepartmentId.Substring(1));
                    if (strnumber > 5000)
                    {
                        return BadRequest(new Status(400, "Out Of Range Id", null));
                    }
                }

                department.DepartmentId = IdGenerator.GenerateNextId(1, "D0000");
            }

            department.Created_Date = DateTime.Now;
            _context.Departments.Add(department);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DepartmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(200, "Created Success", CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department)));
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            if (_context.Departments == null)
            {
                return NotFound(new Status(404, "Not Found Id To Delete"));
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new Status(404, "Not Found Id To Delete"));
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok(new Status(201, "Delete Success", department));
        }

        private bool DepartmentExists(string id)
        {
            return (_context.Departments?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }


    }
}
