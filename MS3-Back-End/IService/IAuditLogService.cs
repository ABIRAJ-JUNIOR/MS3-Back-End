﻿using MS3_Back_End.DTOs.RequestDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;

namespace MS3_Back_End.IService
{
    public interface IAuditLogService
    {
        Task<AuditLogResponceDTO> AddAuditLog(AuditLogRequestDTO auditLog);

    }
}
