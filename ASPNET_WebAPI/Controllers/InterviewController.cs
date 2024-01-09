using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_WebAPI.Models.Data;
using ASPNET_WebAPI.Models.Domains;

namespace ASPNET_WebAPI.Controllers
{
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
            return await _context.Interview.ToListAsync();
        }

        // GET: api/Interview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interview>> GetInterview(int id)
        {
            if (_context.Interview == null)
            {
                return NotFound();
            }
            var interview = await _context.Interview.FindAsync(id);

            if (interview == null)
            {
                return NotFound();
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

            return NoContent();
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
            _context.Interview.Add(interview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterview", new { id = interview.InterviewId }, interview);
        }

        // DELETE: api/Interview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
            if (_context.Interview == null)
            {
                return NotFound();
            }
            var interview = await _context.Interview.FindAsync(id);
            if (interview == null)
            {
                return NotFound();
            }

            _context.Interview.Remove(interview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterviewExists(int id)
        {
            return (_context.Interview?.Any(e => e.InterviewId == id)).GetValueOrDefault();
        }
    }
}
