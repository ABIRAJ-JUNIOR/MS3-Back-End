using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;

namespace MS3_Back_End.DTOs.ResponseDTOs.Admin
{
    public class AdminWithRoleDTO
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;

        public ICollection<AuditLogResponseDTO>? AuditLogs { get; set; }
    }
}
