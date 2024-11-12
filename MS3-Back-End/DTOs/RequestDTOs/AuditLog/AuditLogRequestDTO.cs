using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.AuditLog
{
    public class AuditLogRequestDTO
    {
        public string Action { get; set; } = string.Empty;
        public DateOnly ActionDate { get; set; }
        public string Details { get; set; } = string.Empty;

        public Guid AdminId { get; set; }

        //Reference
        public Admin? Admin { get; set; }
    }
}
