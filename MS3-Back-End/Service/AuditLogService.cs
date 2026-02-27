using Azure.Core;
using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AuditLogService> _logger;

        public AuditLogService(IAuditLogRepository auditLogRepository, IAdminRepository adminRepository, ILogger<AuditLogService> logger)
        {
            _auditLogRepository = auditLogRepository;
            _adminRepository = adminRepository;
            _logger = logger;
        }

        public async Task<string> AddAuditLog(AuditLogRequestDTO auditLog)
        {
            if (auditLog == null)
            {
                throw new ArgumentNullException(nameof(auditLog));
            }

            var adminData = await _adminRepository.GetAdminById(auditLog.AdminId);
            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {AdminId}", auditLog.AdminId);
                throw new KeyNotFoundException("Admin not found");
            }

            var newAuditLog = new AuditLog
            {
                Action = auditLog.Action,
                Details = auditLog.Details,
                ActionDate = DateTime.Now,
                AdminId = auditLog.AdminId,
            };

            var data = await _auditLogRepository.AddAuditLog(newAuditLog);

            _logger.LogInformation("Audit log added successfully with Id: {Id}", data.Id);

            return "Audit log added successfully";
        }

        public async Task<ICollection<AuditLogResponseDTO>> GetAllAuditlogs()
        {
            var data = await _auditLogRepository.GetAllAuditlogs();
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No audit logs found");
                throw new KeyNotFoundException("No audit logs found");
            }

            var returnData = data.Select(x => new AuditLogResponseDTO
            {
                Action = x.Action,
                Details = x.Details,
                Id = x.Id,
                ActionDate = x.ActionDate,
                AdminId = x.AdminId,
                AdminResponse = new AdminResponseDTO
                {
                    Id = x.Admin!.Id,
                    Nic = x.Admin.Nic,
                    FirstName = x.Admin.FirstName,
                    LastName = x.Admin.LastName,
                    Phone = x.Admin.Phone,
                    CreatedDate = x.Admin.CreatedDate,
                    UpdatedDate = x.Admin.UpdatedDate,
                    IsActive = x.Admin.IsActive,
                }
            }).ToList();

            return returnData;
        }

        public async Task<ICollection<AuditLogResponseDTO>> GetAuditLogsbyAdminId(Guid id)
        {
            var data = await _auditLogRepository.GetAuditLogsbyAdminId(id);
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No audit logs found for AdminId: {AdminId}", id);
                throw new KeyNotFoundException("No audit logs found for the specified AdminId");
            }

            var returnData = data.Select(x => new AuditLogResponseDTO
            {
                Details = x.Details,
                Id = x.Id,
                ActionDate = x.ActionDate,
                AdminId = x.AdminId,
                Action = x.Action,
                AdminResponse = new AdminResponseDTO
                {
                    Id = x.Admin!.Id,
                    Nic = x.Admin.Nic,
                    FirstName = x.Admin.FirstName,
                    LastName = x.Admin.LastName,
                    Phone = x.Admin.Phone,
                    CreatedDate = x.Admin.CreatedDate,
                    UpdatedDate = x.Admin.UpdatedDate,
                    IsActive = x.Admin.IsActive,
                }
            }).ToList();

            return returnData;
        }

        public async Task<AuditLogResponseDTO> GetAuditLogByID(Guid id)
        {
            var data = await _auditLogRepository.GetAuditLogByID(id);
            if (data == null)
            {
                _logger.LogWarning("Audit log not found for Id: {Id}", id);
                throw new KeyNotFoundException("Audit log not found");
            }

            var returnData = new AuditLogResponseDTO
            {
                Action = data.Action,
                Details = data.Details,
                Id = data.Id,
                ActionDate = data.ActionDate,
                AdminId = data.AdminId,
                AdminResponse = new AdminResponseDTO
                {
                    Id = data.Admin!.Id,
                    Nic = data.Admin.Nic,
                    FirstName = data.Admin.FirstName,
                    LastName = data.Admin.LastName,
                    Phone = data.Admin.Phone,
                    CreatedDate = data.Admin.CreatedDate,
                    UpdatedDate = data.Admin.UpdatedDate,
                    IsActive = data.Admin.IsActive,
                }
            };

            _logger.LogInformation("Audit log retrieved successfully with Id: {Id}", id);

            return returnData;
        }
    }
}
