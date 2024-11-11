namespace MS3_Back_End.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;

        public Guid UserRoleId { get; set; }

        //Reference
        public UserRole? UserRole { get; set; }
        public ICollection<AuditLog>? AuditLogs { get; set; }

    }
}
