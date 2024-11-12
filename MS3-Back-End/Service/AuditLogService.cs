using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class AuditLogService: IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogResponceDTO> AddAuditLog(AuditLogRequestDTO auditLog)
        {

            var AuditLog=new AuditLog()
            {
                Action= auditLog.Action,
                Details= auditLog.Details,
                ActionDate= auditLog.ActionDate,
                AdminId= auditLog.AdminId,
            };

            var data = await _auditLogRepository.AddAuditLog(AuditLog);

            var returndata = new AuditLogResponceDTO()
            {
                AdminId = data.AdminId,
                ActionDate = data.ActionDate,
                Details = data.Details,
                Id = data.Id,
                Action = auditLog.Action,
            };

            return returndata;
        }

    }
}
