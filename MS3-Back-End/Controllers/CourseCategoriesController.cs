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
    public class CourseCategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CourseCategoriesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/CourseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseCategory>>> GetCourseCategories()
        {
            return await _context.CourseCategories.ToListAsync();
        }

        // GET: api/CourseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseCategory>> GetCourseCategory(int id)
        {
            var courseCategory = await _context.CourseCategories.FindAsync(id);

            if (courseCategory == null)
            {
                return NotFound();
            }

            return courseCategory;
        }

        // PUT: api/CourseCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseCategory(int id, CourseCategory courseCategory)
        {
            if (id != courseCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(courseCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseCategoryExists(id))
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

        // POST: api/CourseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseCategory>> PostCourseCategory(CourseCategory courseCategory)
        {
            _context.CourseCategories.Add(courseCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseCategory", new { id = courseCategory.Id }, courseCategory);
        }

        // DELETE: api/CourseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseCategory(int id)
        {
            var courseCategory = await _context.CourseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            _context.CourseCategories.Remove(courseCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseCategoryExists(int id)
        {
            return _context.CourseCategories.Any(e => e.Id == id);
        }
    }
}
