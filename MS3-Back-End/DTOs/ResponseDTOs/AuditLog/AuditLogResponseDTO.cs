using MS3_Back_End.DTOs.ResponseDTOs.Admin;

namespace MS3_Back_End.DTOs.ResponseDTOs.AuditLog
{
    public class AuditLogResponseDTO
    {
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string Details { get; set; } = string.Empty;

        public Guid AdminId { get; set; }

        public AdminResponseDTO AdminResponse { get; set; } = new AdminResponseDTO();
    }
}
