using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;

namespace MS3_Back_End.IService
{
    public interface IAuditLogService
    {
        Task<AuditLogResponceDTO> AddAuditLog(AuditLogRequestDTO auditLog);
        Task<ICollection<AuditLogResponceDTO>> GetAllAuditlogs();

        Task<ICollection<AuditLogResponceDTO>> GetAuditLogsbyAdminId(Guid id);

        Task<AuditLogResponceDTO> GetAuditLogByID(Guid id);
    }
}
