namespace MS3_Back_End.Entities
{
    public class Admin
    {
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public DateTime LastLogin { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; }

        //Reference
        public ICollection<AuditLog>? auditLogs { get; set; }

    }

    public enum Roles
    {
        SuperAdmin = 1,
        Admin = 2,
        Student = 3
    }
}
