using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssesmentsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AssesmentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Assesments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assesment>>> GetAssesments()
        {
            return await _context.Assesments.ToListAsync();
        }

        // GET: api/Assesments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assesment>> GetAssesment(Guid id)
        {
            var assesment = await _context.Assesments.FindAsync(id);

            if (assesment == null)
            {
                return NotFound();
            }

            return assesment;
        }

        // PUT: api/Assesments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssesment(Guid id, Assesment assesment)
        {
            if (id != assesment.Id)
            {
                return BadRequest();
            }

            _context.Entry(assesment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssesmentExists(id))
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

        // POST: api/Assesments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assesment>> PostAssesment(Assesment assesment)
        {
            _context.Assesments.Add(assesment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssesment", new { id = assesment.Id }, assesment);
        }

        // DELETE: api/Assesments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssesment(Guid id)
        {
            var assesment = await _context.Assesments.FindAsync(id);
            if (assesment == null)
            {
                return NotFound();
            }

            _context.Assesments.Remove(assesment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssesmentExists(Guid id)
        {
            return _context.Assesments.Any(e => e.Id == id);
        }
    }
}
