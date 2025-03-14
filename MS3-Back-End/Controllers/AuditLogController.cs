﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpPost]
        public async Task<ActionResult<AuditLogResponseDTO>> AddAuditLog(AuditLogRequestDTO auditLogRequestDTO)
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
                _logger.Error(ex, "Error adding audit log");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AuditLogResponseDTO>>> GetAllAuditLogs()
        {
            try
            {
                var data = await _auditLogService.GetAllAuditlogs();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all audit logs");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Get-AuditLogs-By/{adminId}")]
        public async Task<ActionResult<IEnumerable<AuditLogResponseDTO>>> GetAuditLogsByAdminId(Guid adminId)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogsbyAdminId(adminId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting audit logs by admin id {adminId}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Get-AuditLog-By/{id}")]
        public async Task<ActionResult<AuditLogResponseDTO>> GetAuditLogById(Guid id)
        {
            try
            {
                var data = await _auditLogService.GetAuditLogByID(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting audit log by id {id}");
                return BadRequest(ex.Message);
            }
        }
    }
}
