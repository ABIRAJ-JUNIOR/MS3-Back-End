using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAuditLogRepository

    {
        Task<AuditLog> AddAuditLog(AuditLog auditLog);
        Task<List<AuditLog>> GetAllAuditlogs();


    }
}
