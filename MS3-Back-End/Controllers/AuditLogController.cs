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
        [HttpGet]
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

        public async Task<IActionResult> GetAuditLogByID(Guid id)
        {
            var data=await _auditLogService.GetAuditLogByID(id);
            return Ok(data);
        }


    }
}
