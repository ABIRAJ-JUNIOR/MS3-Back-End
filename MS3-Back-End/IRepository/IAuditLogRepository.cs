using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAuditLogRepository

    {
        Task<AuditLog> AddAuditLog(AuditLog auditLog);
        Task<ICollection<AuditLog>> GetAllAuditlogs();
        Task<ICollection<AuditLog>> GetAuditLogsbyAdminId(Guid id);
        Task<AuditLog> GetAuditLogByID(Guid id);
    }
}
