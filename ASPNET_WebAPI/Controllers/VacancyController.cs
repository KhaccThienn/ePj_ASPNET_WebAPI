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

namespace ASPNET_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly DataContext _context;

        public VacancyController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Vacancy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetVacancies()
        {
            if (_context.Vacancies == null)
            {
                return NotFound(new Status(500, "Entry set _context.Vacancies is null"));
            }
            return await _context.Vacancies.Include(x => x.OwnedBy).Include(x => x.Applicant_Vacancy).Include(x => x.Department).ToListAsync();
        }

        // GET: api/Vacancy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacancy>> GetVacancy(string id)
        {
            if (_context.Vacancies == null)
            {
                return NotFound(new Status(404, "Entry set _context.Vacancies is null"));
            }
            var vacancy = await _context.Vacancies.Include(x => x.OwnedBy).Include(x => x.Applicant_Vacancy).Include(x => x.Department).FirstOrDefaultAsync(x => x.Vacancy_Number == id);

            if (vacancy == null)
            {
                return NotFound(new Status(404, "Cannot Found Vacancy"));
            }

            return vacancy;
        }

        // PUT: api/Vacancy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy(string id, Vacancy vacancy)
        {
            if (id != vacancy.Vacancy_Number)
            {
                return BadRequest(new Status(400, "Id does not match"));
            }

            vacancy.Updated_Date = DateTime.Now;
            _context.Entry(vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacancyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(204, "Update Success", vacancy));
        }

        // POST: api/Vacancy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacancy>> PostVacancy(Vacancy vacancy)
        {
            if (_context.Vacancies == null)
            {
                return BadRequest(new Status(500, "Entity set 'DataContext.Vacancies'  is null."));
            
            }
            var latest = await _context.Vacancies.OrderByDescending(x => x.Vacancy_Number).FirstOrDefaultAsync();
            int strnumber = 0;
            if (latest != null && string.IsNullOrEmpty(vacancy.Vacancy_Number))
            {
                strnumber = int.Parse(latest.Vacancy_Number.Substring(1));

                if (strnumber > 5000)
                {
                    return BadRequest(new Status(400, "Out Of Range Id", null));
                }
                vacancy.Vacancy_Number = IdGenerator.GenerateNextId(1, latest.Vacancy_Number);
            }
            else
            {
                if (!string.IsNullOrEmpty(vacancy.Vacancy_Number))
                {
                    strnumber = int.Parse(vacancy.Vacancy_Number.Substring(1));
                    if (strnumber > 5000)
                    {
                        return BadRequest(new Status(400, "Out Of Range Id", null));
                    }
                }

                vacancy.Vacancy_Number = IdGenerator.GenerateNextId(1, "V0000");
            }

            vacancy.Date_Created = DateTime.Now;
            _context.Vacancies.Add(vacancy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VacancyExists(vacancy.Vacancy_Number))
                {
                    return Conflict(new Status(500, "Conflicted !"));
                }
                else
                {
                    throw;
                }
            }
            return Ok(new Status(200, "Create Success !", CreatedAtAction("GetVacancy", new { id = vacancy.Vacancy_Number }, vacancy)));
        }

        // DELETE: api/Vacancy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(string id)
        {
            if (_context.Vacancies == null)
            {
                return NotFound(new Status(500, "Entry set _context.Vacancies is null"));
            }
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound(new Status(404, "Vancancy is Null, cannot delete"));
            }

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return Ok(new Status(200, "Delete Success !", vacancy));
        }

        private bool VacancyExists(string id)
        {
            return (_context.Vacancies?.Any(e => e.Vacancy_Number == id)).GetValueOrDefault();
        }
    }
}
