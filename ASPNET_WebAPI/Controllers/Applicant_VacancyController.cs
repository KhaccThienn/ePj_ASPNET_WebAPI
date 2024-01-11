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
using ServiceStack.Redis;
using Microsoft.AspNetCore.Authorization;

namespace ASPNET_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class Applicant_VacancyController : ControllerBase
    {
        private readonly DataContext _context;

        public Applicant_VacancyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Applicant_Vacancy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applicant_Vacancy>>> GetApplicant_Vacancy()
        {
            if (_context.Applicant_Vacancy == null)
            {
                return NotFound(new Status(500, "Entry set _context.Applicant_Vacancy is null here "));
            }
            return await _context.Applicant_Vacancy.OrderByDescending(x => x.DateAttached).Include(x => x.Vacancy).Include(x => x.Applicant).Include(x => x.Interviews).ToListAsync();
        }

        // GET: api/Applicant_Vacancy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant_Vacancy>> GetApplicant_Vacancy(int id)
        {
            if (_context.Applicant_Vacancy == null)
            {
                return NotFound(new Status(500, "Entry set _context.Applicant_Vacancy is null here "));
            }
            var applicant_Vacancy = await _context.Applicant_Vacancy.Include(x => x.Vacancy).Include(x => x.Applicant).Include(x => x.Interviews).FirstOrDefaultAsync(x => x.Id == id);

            if (applicant_Vacancy == null)
            {
                return NotFound(new Status(404, "Not found entity Applicant_Vacancy"));
            }

            return applicant_Vacancy;
        }

        // PUT: api/Applicant_Vacancy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant_Vacancy(int id, Applicant_Vacancy applicant_Vacancy)
        {
            if (id != applicant_Vacancy.Id)
            {
                return BadRequest();
            }
            _context.Entry(applicant_Vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Applicant_VacancyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Applicant_Vacancy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Applicant_Vacancy>> PostApplicant_Vacancy([FromBody] Applicant_Vacancy applicant_Vacancy)
        {
            if (_context.Applicant_Vacancy == null)
            {
                return Problem("Entity set 'DataContext.Applicant_Vacancy'  is null.");
            }
            applicant_Vacancy.DateAttached = DateTime.Now;

            var currentVacancy = await _context.Vacancies.Include(x => x.Applicant_Vacancy).FirstOrDefaultAsync(x => x.Vacancy_Number == applicant_Vacancy.VacancyId);

            var existedApplicantVacancy = await _context.Applicant_Vacancy.FirstOrDefaultAsync(x => x.VacancyId == applicant_Vacancy.VacancyId && x.ApplicantId == applicant_Vacancy.ApplicantId);
            if (existedApplicantVacancy != null)
            {
                return BadRequest(new Status(400, "Already Have Applicant - Vacancy"));
            }

            if (currentVacancy.Applicant_Vacancy.Count >= currentVacancy.NumberOfJobs)
            {
                currentVacancy.Status = Models.Enums.VacancyStatus.CLOSED;
                currentVacancy.Closed_Date = DateTime.Now;
                _context.Update(currentVacancy);
                await _context.SaveChangesAsync();

                return Ok(new Status(400, "Cannot Add More", CreatedAtAction("GetApplicant_Vacancy", new { id = applicant_Vacancy.Id }, applicant_Vacancy)));

            }
            else
            {
                _context.Applicant_Vacancy.Add(applicant_Vacancy);
            }

            await _context.SaveChangesAsync();
            return Ok(new Status(200, "Add successfully", CreatedAtAction("GetApplicant_Vacancy", new { id = applicant_Vacancy.Id }, applicant_Vacancy)));
        }

        // DELETE: api/Applicant_Vacancy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant_Vacancy(int id)
        {
            if (_context.Applicant_Vacancy == null)
            {
                return NotFound(new Status(400, "Context Applicant_Vacancy not found"));
            }
            var applicant_Vacancy = await _context.Applicant_Vacancy.FindAsync(id);
            if (applicant_Vacancy == null)
            {
                return NotFound(new Status(404, "Not found Applicant_Vacancy"));
            }

            _context.Applicant_Vacancy.Remove(applicant_Vacancy);
            await _context.SaveChangesAsync();

            return Ok(new Status(204, "Deleted successfully", applicant_Vacancy));
        }

        private bool Applicant_VacancyExists(int id)
        {
            return (_context.Applicant_Vacancy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
