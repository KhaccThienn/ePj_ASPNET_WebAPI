using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_WebAPI.Models.Data;
using ASPNET_WebAPI.Models.Domains;
using AutoMapper;
using ASPNET_WebAPI.Models.Status;
using ASPNET_WebAPI.Models.DTOs;
using ASPNET_WebAPI.Utils;

namespace ASPNET_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public ApplicantController(DataContext context, IWebHostEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }

        // GET: api/Applicant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicants()
        {
            if (_context.Applicants == null)
            {
                return NotFound();
            }
            return await _context.Applicants.OrderByDescending(x => x.Created_Date).Include(x => x.Applicant_Vacancy).ToListAsync();
        }

        [HttpGet("validApplicant")]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicantsValid()
        {
            if (_context.Applicants == null)
            {
                return NotFound();
            }
            return await _context.Applicants.OrderByDescending(x => x.Created_Date).Include(x => x.Applicant_Vacancy).Where(x => x.Status == Models.Enums.ApplicantStatus.IN_PROCESS || x.Status == Models.Enums.ApplicantStatus.NOT_IN_PROCESS).ToListAsync();
        }


        // GET: api/Applicant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(string id)
        {
            if (_context.Applicants == null)
            {
                return NotFound(new Status(404, "Entry _context.Applicants is null here"));
            }
            var applicant = await _context.Applicants.Include(x => x.Applicant_Vacancy).FirstOrDefaultAsync(x => x.Applicant_Id == id);

            if (applicant == null)
            {
                return NotFound(new Status(404, "Not Found Applicant"));
            }

            return applicant;
        }

        [HttpGet("search/{id}")]
        public async Task<ActionResult<IEnumerable<Applicant>>> GetApplicantsss(string id)
        {
            if (_context.Applicants == null)
            {
                return NotFound(new Status(404, "Entry _context.Applicants is null here"));
            }
            var applicant = await _context.Applicants.OrderByDescending(x => x.Created_Date).Include(x => x.Applicant_Vacancy).ToListAsync();

            if (applicant == null)
            {
                return NotFound(new Status(404, "Not Found Applicant"));
            }

            return applicant;
        }

        // PUT: api/Applicant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant(string id, [FromForm] UpdateApplicantImage applicant)
        {
            if (id != applicant.Applicant_Id)
            {
                return BadRequest(new Status(404, "Id does not Match", null));
            }
            if (_context.Applicants.Any(x => x.EmailId == applicant.EmailId && x.Applicant_Id != applicant.Applicant_Id) || _context.Employees.Any(x => x.EmailId == applicant.EmailId))
            {
                return BadRequest(new Status(400, "Email already in use", null));
            }

            if (applicant.ImageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "applicant", applicant.ImageFile.FileName);
                var oldFileName = applicant.OldImage.Split($"{HttpContext.Request.Host.Value}/uploads/applicant/");
                Console.WriteLine($"Emp old File: {oldFileName[1]}");
                var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "applicant", oldFileName[1]);
                if (System.IO.File.Exists(pathOldFile))
                {
                    System.IO.File.Delete(pathOldFile);
                }
                using (var file = System.IO.File.Create(path))
                {
                    await applicant.ImageFile.CopyToAsync(file);
                }
                applicant.Avatar = $"https://{HttpContext.Request.Host.Value}/uploads/applicant/{applicant.ImageFile.FileName}";
            }
            else
            {
                applicant.Avatar = applicant.OldImage;
            }
            applicant.Updated_Date = DateTime.Now;
            var app = _mapper.Map<Applicant>(applicant);
            _context.Entry(app).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
                {
                    return NotFound(new Status(404, "Id Not Found", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(201, "Update Success", app));
        }

        // POST: api/Applicant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant([FromForm] ApplicantImage applicant)
        {
            if (_context.Applicants == null)
            {
                return BadRequest(new Status(500, "Entity set 'DataContext.Applicants'  is null.", null));
            }

            var latest = await _context.Applicants.OrderByDescending(x => x.Applicant_Id).FirstOrDefaultAsync();
            int strnumber = 0;
            if (latest != null && string.IsNullOrEmpty(applicant.Applicant_Id))
            {
                strnumber = int.Parse(latest.Applicant_Id.Substring(1));

                if (strnumber > 5000)
                {
                    return BadRequest(new Status(400, "Out Of Range Id", null));
                }
                applicant.Applicant_Id = IdGenerator.GenerateNextId(1, latest.Applicant_Id);
            }
            else
            {
                if (!string.IsNullOrEmpty(applicant.Applicant_Id))
                {
                    strnumber = int.Parse(applicant.Applicant_Id.Substring(1));
                    if (strnumber > 5000)
                    {
                        return BadRequest(new Status(400, "Out Of Range Id", null));
                    }
                }

                applicant.Applicant_Id = IdGenerator.GenerateNextId(1, "A0000");
            }

            if (_context.Applicants.Any(x => x.EmailId == applicant.EmailId) || _context.Employees.Any(x => x.EmailId == applicant.EmailId))
            {
                return BadRequest(new Status(400, "Email already in use", null));
            }

            if (applicant.ImageFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "applicant", applicant.ImageFile.FileName);
                using (var file = System.IO.File.Create(path))
                {
                    await applicant.ImageFile.CopyToAsync(file);
                }
                applicant.Avatar = $"https://{HttpContext.Request.Host.Value}/uploads/applicant/{applicant.ImageFile.FileName}";
            }

            applicant.Created_Date = DateTime.Now;
            var app = _mapper.Map<Applicant>(applicant);
            _context.Applicants.Add(app);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicantExists(applicant.Applicant_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Status(200, "Created Success", CreatedAtAction("GetApplicant", new { id = applicant.Applicant_Id }, applicant)));
        }

        // DELETE: api/Applicant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(string id)
        {
            if (_context.Applicants == null)
            {
                return NotFound(new Status(500, "Entity set 'DataContext.Applicants'  is null.", null));
            }
            var applicant = await _context.Applicants.FindAsync(id);
            var oldFileName = applicant.Avatar.Split($"{HttpContext.Request.Host.Value}/uploads/applicant/");
            Console.WriteLine($"Emp old File: {oldFileName[1]}");
            var pathOldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "applicant", oldFileName[1]);
            if (System.IO.File.Exists(pathOldFile))
            {
                System.IO.File.Delete(pathOldFile);
            }
            if (applicant == null)
            {
                return NotFound(new Status(404, "Not Found Applicant To Delete", applicant));
            }

            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();

            return Ok(new Status(204, "Delete Success", applicant));
        }

        private bool ApplicantExists(string? id)
        {
            return (_context.Applicants?.Any(e => e.Applicant_Id == id)).GetValueOrDefault();
        }
    }
}
