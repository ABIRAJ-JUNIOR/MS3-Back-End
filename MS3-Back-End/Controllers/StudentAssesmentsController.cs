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
    public class StudentAssesmentsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StudentAssesmentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/StudentAssesments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentAssesment>>> GetStudentAssesments()
        {
            return await _context.StudentAssesments.ToListAsync();
        }

        // GET: api/StudentAssesments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentAssesment>> GetStudentAssesment(Guid id)
        {
            var studentAssesment = await _context.StudentAssesments.FindAsync(id);

            if (studentAssesment == null)
            {
                return NotFound();
            }

            return studentAssesment;
        }

        // PUT: api/StudentAssesments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentAssesment(Guid id, StudentAssesment studentAssesment)
        {
            if (id != studentAssesment.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentAssesment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentAssesmentExists(id))
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

        // POST: api/StudentAssesments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentAssesment>> PostStudentAssesment(StudentAssesment studentAssesment)
        {
            _context.StudentAssesments.Add(studentAssesment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentAssesment", new { id = studentAssesment.Id }, studentAssesment);
        }

        // DELETE: api/StudentAssesments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAssesment(Guid id)
        {
            var studentAssesment = await _context.StudentAssesments.FindAsync(id);
            if (studentAssesment == null)
            {
                return NotFound();
            }

            _context.StudentAssesments.Remove(studentAssesment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentAssesmentExists(Guid id)
        {
            return _context.StudentAssesments.Any(e => e.Id == id);
        }
    }
}
