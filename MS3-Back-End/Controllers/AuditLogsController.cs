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
    public class AuditLogsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AuditLogsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
        {
            return await _context.AuditLogs.ToListAsync();
        }

        // GET: api/AuditLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLog>> GetAuditLog(Guid id)
        {
            var auditLog = await _context.AuditLogs.FindAsync(id);

            if (auditLog == null)
            {
                return NotFound();
            }

            return auditLog;
        }

        // PUT: api/AuditLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuditLog(Guid id, AuditLog auditLog)
        {
            if (id != auditLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(auditLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(id))
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

        // POST: api/AuditLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuditLog>> PostAuditLog(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuditLog", new { id = auditLog.Id }, auditLog);
        }

        // DELETE: api/AuditLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuditLog(Guid id)
        {
            var auditLog = await _context.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return NotFound();
            }

            _context.AuditLogs.Remove(auditLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuditLogExists(Guid id)
        {
            return _context.AuditLogs.Any(e => e.Id == id);
        }
    }
}
