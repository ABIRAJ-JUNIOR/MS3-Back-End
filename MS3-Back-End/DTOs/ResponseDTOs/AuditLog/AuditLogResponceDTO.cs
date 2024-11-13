using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.AuditLog
{
    public class AuditLogResponceDTO
    {

        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateOnly ActionDate { get; set; }
        public string Details { get; set; } = string.Empty;

        public Guid AdminId { get; set; }

        //Reference
        public Admin? Admin { get; set; }

    }
}
