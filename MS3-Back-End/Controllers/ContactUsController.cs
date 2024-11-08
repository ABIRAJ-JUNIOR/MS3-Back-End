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
    public class ContactUsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ContactUsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/ContactUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetContactUs()
        {
            return await _context.ContactUs.ToListAsync();
        }

        // GET: api/ContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> GetContactUs(Guid id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            return contactUs;
        }

        // PUT: api/ContactUs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactUs(Guid id, ContactUs contactUs)
        {
            if (id != contactUs.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactUs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactUsExists(id))
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

        // POST: api/ContactUs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactUs>> PostContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Add(contactUs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactUs", new { id = contactUs.Id }, contactUs);
        }

        // DELETE: api/ContactUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactUs(Guid id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }

            _context.ContactUs.Remove(contactUs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactUsExists(Guid id)
        {
            return _context.ContactUs.Any(e => e.Id == id);
        }
    }
}
