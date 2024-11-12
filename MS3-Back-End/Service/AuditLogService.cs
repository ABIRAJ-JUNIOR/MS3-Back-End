using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AuditLogService: IAuditLogService
    {
        private readonly IAuditLogService _Auditservice;

        public AuditLogService(IAuditLogService auditservice)
        {
            _Auditservice = auditservice;
        }
    }
}
