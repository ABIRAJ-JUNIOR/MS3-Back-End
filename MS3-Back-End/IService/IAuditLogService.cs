using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;

namespace MS3_Back_End.IService
{
    public interface IAuditLogService
    {
        Task<AuditLogResponceDTO> AddAuditLog(AuditLogRequestDTO auditLog);
        Task<List<AuditLogResponceDTO>> GetAllAuditlogs();

        Task<List<AuditLogResponceDTO>> GetAuditLogsbyAdminId(Guid id);

        Task<AuditLogResponceDTO> GetAuditLogByID(Guid id);
        Task<AuditLogResponceDTO> UpdateAuditLog(Guid auditlogid, AuditLogUpdateRequest auditLogService);

        Task<AuditLogResponceDTO> DeleteAuditlog(Guid id);
    }
}
