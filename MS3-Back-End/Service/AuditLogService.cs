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
    public class AuditLogService: IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IAdminRepository _adminRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository, IAdminRepository adminRepository)
        {
            _auditLogRepository = auditLogRepository;
            _adminRepository = adminRepository;
        }

        public async Task<AuditLogResponceDTO> AddAuditLog(AuditLogRequestDTO auditLog)
        {
            var adminData = await _adminRepository.GetAdminById(auditLog.AdminId);
            if (adminData == null)
            {
                throw new Exception("Admin not found");
            }

            var AuditLog = new AuditLog()
            {
                Action= auditLog.Action,
                Details= auditLog.Details,
                ActionDate= DateTime.Now,
                AdminId= auditLog.AdminId,
            };

            var data = await _auditLogRepository.AddAuditLog(AuditLog);

            var returndata = new AuditLogResponceDTO()
            {
                Id = data.Id,
                AdminId = data.AdminId,
                ActionDate = data.ActionDate,
                Details = data.Details,
                Action = data.Action,
            };

            return returndata;
        }
        public async Task<ICollection<AuditLogResponceDTO>> GetAllAuditlogs()
        {
             var datas= await _auditLogRepository.GetAllAuditlogs();
            var returndatas=datas.Select(x => new AuditLogResponceDTO()
            {
                Action= x.Action,
                Details= x.Details,
                Id= x.Id,
                ActionDate= x.ActionDate,
                AdminId= x.AdminId,
                AdminResponse = new AdminResponseDTO()
                {
                    Id = x.Admin!.Id,
                    Nic = x.Admin.Nic,
                    FirstName = x.Admin.FirstName,
                    LastName = x.Admin.LastName,
                    Phone = x.Admin.Phone,
                    CteatedDate = x.Admin.CteatedDate,
                    UpdatedDate = x.Admin.CteatedDate,
                    IsActive = x.Admin.IsActive,
                }
            }).ToList();
            return returndatas;
        }
        public async Task<ICollection<AuditLogResponceDTO>> GetAuditLogsbyAdminId(Guid id)
        {
            var data =await _auditLogRepository.GetAuditLogsbyAdminId(id);

            var returndata = data.Select(x => new AuditLogResponceDTO()
            {
                Details = x.Details,
                Id = x.Id,
                ActionDate = x.ActionDate,
                AdminId = x.AdminId,
                Action = x.Action,
                AdminResponse = new AdminResponseDTO()
                {
                    Id = x.Admin!.Id,
                    Nic = x.Admin.Nic,
                    FirstName = x.Admin.FirstName,
                    LastName = x.Admin.LastName,
                    Phone = x.Admin.Phone,
                    CteatedDate = x.Admin.CteatedDate,
                    UpdatedDate = x.Admin.CteatedDate,
                    IsActive = x.Admin.IsActive,
                }
            }).ToList();

            return returndata;  
        }

        public async Task<AuditLogResponceDTO> GetAuditLogByID(Guid id)
        {
             var data= await _auditLogRepository.GetAuditLogByID(id);
            var returndata = new AuditLogResponceDTO()
            {
                Action =data.Action,
                Details = data.Details,
                Id = data.Id,
                ActionDate = data.ActionDate,
                AdminId = data.AdminId,
                AdminResponse = new AdminResponseDTO()
                {
                    Id = data.Admin!.Id,
                    Nic = data.Admin.Nic,
                    FirstName = data.Admin.FirstName,
                    LastName = data.Admin.LastName,
                    Phone = data.Admin.Phone,
                    CteatedDate = data.Admin.CteatedDate,
                    UpdatedDate = data.Admin.CteatedDate,
                    IsActive = data.Admin.IsActive,
                }
            };

            return returndata;
        }
    }
}
