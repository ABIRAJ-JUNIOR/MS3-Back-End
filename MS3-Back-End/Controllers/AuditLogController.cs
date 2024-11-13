using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }
        [HttpPost("Audit log")]
        public async Task<IActionResult> AddAuditLog(AuditLogRequestDTO auditLogRequestDTO) 
        {
            try
            {
                var data = await _auditLogService.AddAuditLog(auditLogRequestDTO);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-all-Auditlogs")]
        public async Task<IActionResult> GetallAuditlogs()
        {
            try
            {
                var data = await _auditLogService.GetAllAuditlogs();
                return Ok(data);
            }
            catch (Exception ex) 
            {
              return Ok(ex.Message);
            }
        
        }
        [HttpGet("Get-AuditLogsby-AdminId")]
        public async Task<IActionResult> GetAuditLogsbyAdminId(Guid id)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogsbyAdminId(id);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Get-AuditLogBy-ID")]

        public async Task<IActionResult> GetAuditLogByID(Guid id)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogByID(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Update-Audit-Log")]
        public async Task<IActionResult> UpdateAuditLog(Guid auditlogid, AuditLogUpdateRequest auditLogService)
        {
            try
            {
                var data = await _auditLogService.UpdateAuditLog(auditlogid, auditLogService);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete-Audit-log")]
        public async Task<IActionResult> DeleteAuditlog(Guid id)
        {
            try
            {
                var data = await _auditLogService.DeleteAuditlog(id);
                return Ok("deleted");
            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message);   
            }
        }



    }
}
