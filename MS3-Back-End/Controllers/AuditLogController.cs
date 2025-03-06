using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<AuditLogController> _logger;

        public AuditLogController(IAuditLogService auditLogService, ILogger<AuditLogController> logger)
        {
            _auditLogService = auditLogService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AuditLogResponceDTO>> AddAuditLog(AuditLogRequestDTO auditLogRequestDTO)
        {
            if (auditLogRequestDTO == null)
            {
                return BadRequest("Audit log data is required.");
            }

            try
            {
                var data = await _auditLogService.AddAuditLog(auditLogRequestDTO);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding audit log");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AuditLogResponceDTO>>> GetAllAuditLogs()
        {
            try
            {
                var data = await _auditLogService.GetAllAuditlogs();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all audit logs");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Get-AuditLogs-By/{adminId}")]
        public async Task<ActionResult<IEnumerable<AuditLogResponceDTO>>> GetAuditLogsByAdminId(Guid adminId)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogsbyAdminId(adminId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting audit logs by admin id {adminId}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Get-AuditLog-By/{id}")]
        public async Task<ActionResult<AuditLogResponceDTO>> GetAuditLogById(Guid id)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogByID(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting audit log by id {id}");
                return BadRequest(ex.Message);
            }
        }
    }
}
