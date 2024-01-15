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
using Microsoft.AspNetCore.Cors;
using ASPNET_WebAPI.Models.Enums;

namespace ASPNET_WebAPI.Controllers
{
    [EnableCors("AllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        private readonly DataContext _context;

        public InterviewController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Interview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterview()
        {
            if (_context.Interview == null)
            {
                return NotFound();
            }
            return await _context.Interview.Include(x => x.Applicant_Vacancy).Include(x => x.Vacancy).Include(x => x.Applicant).Include(x => x.Employee).ToListAsync();
        }

        [HttpGet("byUserId/{id}")]
        public async Task<ActionResult<IEnumerable<Interview>>> GetInterviews(string id)
        {
            if (_context.Interview == null)
            {
                return NotFound();
            }
            return await _context.Interview.Include(x => x.Applicant_Vacancy).Include(x => x.Vacancy).Include(x => x.Applicant).Include(x => x.Employee).Where(x => x.EmployeeNumber == id).ToListAsync();
        }

        // GET: api/Interview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interview>> GetInterview(int id)
        {
            if (_context.Interview == null)
            {
                return NotFound(new Status(404, "Entry Interview is Null", null));
            }
            var interview = await _context.Interview.Include(x => x.Employee).Include(x => x.Vacancy).Include(x => x.Applicant).Include(x => x.Applicant_Vacancy).FirstOrDefaultAsync(x => x.InterviewId == id);

            if (interview == null)
            {
                return NotFound(new Status(404, "Cannot interview Employee", null));
            }

            return interview;
        }

        // PUT: api/Interview/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterview(int id, Interview interview)
        {
            if (id != interview.InterviewId)
            {
                return BadRequest();
            }
            interview.Updated_Date = DateTime.Now;
            _context.Entry(interview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(201, "Update Success", interview));
        }

        [HttpPut("updateStatus/{id}/{status}")]
        public async Task<IActionResult> UpdateInterviewStatus(int id, int status)
        {
            var interview = await _context.Interview.FirstOrDefaultAsync(x => x.InterviewId == id);
            if (interview == null)
            {
                return NotFound(new Status(404, "Not Found Interview Details To Update"));
            }
            interview.InterviewStatuss = (InterviewStatus?)status;
            interview.Updated_Date = DateTime.Now;
            _context.Entry(interview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Status(201, "Update Interview Status Success", interview));
        }

        // POST: api/Interview
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interview>> PostInterview(Interview interview)
        {
            if (_context.Interview == null)
            {
                return Problem("Entity set 'DataContext.Interview'  is null.");
            }
            if (DateTime.Compare(DateTime.Now, interview.InterviewDate) > 0)
            {
                return BadRequest(new Status(400, "The interview date must be in the future"));
            }
            interview.Created_Date = DateTime.Now;
            _context.Interview.Add(interview);
            await _context.SaveChangesAsync();

            return Ok(new Status(200, "Create Success", CreatedAtAction("GetInterview", new { id = interview.InterviewId }, interview))) ;
        }

        // DELETE: api/Interview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            if (_context.Interview == null)
            {
                return NotFound(new Status(404, "context Interview Is Null"));
            }
            var interview = await _context.Interview.FindAsync(id);
            if (interview == null)
            {
                return NotFound(new Status(404, "Not Found Record To Delete"));
            }

            _context.Interview.Remove(interview);
            await _context.SaveChangesAsync();

            return Ok(new Status(201, "Delete Success", interview));
        }

        private bool InterviewExists(int id)
        {
            return (_context.Interview?.Any(e => e.InterviewId == id)).GetValueOrDefault();
        }
    }
}
