using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDBContext _dbContext;

        public AuditLogRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AuditLog> AddAuditLog(AuditLog auditLog)
        {
            var data=await _dbContext.AuditLogs.AddAsync(auditLog);
            return data.Entity;
        }   
        public async Task<List<AuditLog>> GetAllAuditlogs(List<AuditLog> auditLogs)
        {
            var data= await _dbContext.AuditLogs.ToListAsync();
            return data;
        }
    }
}
