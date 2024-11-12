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
        public async Task<List<AuditLog>> GetAllAuditlogs()
        {
            var data= await _dbContext.AuditLogs.ToListAsync();
            return data;
        }
        public async Task<List<AuditLog>> GetAuditLogsbyAdminId(Guid id)
        {
            var data= await _dbContext.AuditLogs.Where(a=>a.AdminId == id).ToListAsync();
            return data;
        }

        public async Task<AuditLog> GetAuditLogByID(Guid id)
        {
            var data=_dbContext.AuditLogs.SingleOrDefault(a=> a.Id == id);
            return data;
        }

        public async Task<AuditLog> UpdateAuditLog(AuditLog auditLog)
        {
            var data =  _dbContext.AuditLogs.Update(auditLog);
            return data.Entity;
        }
    }
}
