using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;

namespace MS3_Back_End.IService
{
    public interface IAuditLogService
    {
        Task<string> AddAuditLog(AuditLogRequestDTO auditLog);
        Task<ICollection<AuditLogResponseDTO>> GetAllAuditlogs();

        Task<ICollection<AuditLogResponseDTO>> GetAuditLogsbyAdminId(Guid id);

        Task<AuditLogResponseDTO> GetAuditLogByID(Guid id);
    }
}
