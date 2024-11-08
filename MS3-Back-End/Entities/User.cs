using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Nic { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string? Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;

        //Reference
        public Address? Address { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Feedbacks>? Feedbacks { get; set; }
        public ICollection<StudentAssesment>? Assesments { get; set; }
        public ICollection<AuditLog>? AuditLogs { get; set; }
    }

    public enum Roles
    {
        SuperAdmin = 1,
        Admin = 2,
        Student = 3
    }
}
