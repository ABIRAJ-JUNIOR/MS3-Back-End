﻿using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
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
        public async Task<List<AuditLogResponceDTO>> GetAllAuditlogs()
        {
             var datas= await _auditLogRepository.GetAllAuditlogs();
            var returndatas=datas.Select(x => new AuditLogResponceDTO()
            {
                Action= x.Action,
                Details= x.Details,
                Id= x.Id,
                ActionDate= x.ActionDate,
                AdminId= x.AdminId,
                        
            }).ToList();
            return returndatas;
        }
        public async Task<List<AuditLogResponceDTO>> GetAuditLogsbyAdminId(Guid id)
        {
            var data =await _auditLogRepository.GetAuditLogsbyAdminId(id);

            var returndata = data.Select(x => new AuditLogResponceDTO()
            {
                Details = x.Details,
                Id = x.Id,
                ActionDate = x.ActionDate,
                AdminId = x.AdminId,
                Action = x.Action,
            }).ToList();

            return returndata;  
        }


    }
}
