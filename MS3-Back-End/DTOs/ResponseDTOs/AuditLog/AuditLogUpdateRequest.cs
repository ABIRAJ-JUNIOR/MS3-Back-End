namespace MS3_Back_End.DTOs.ResponseDTOs.AuditLog
{
    public class AuditLogUpdateRequest
    {
        public string Action { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string Details { get; set; } = string.Empty;
    }
}
