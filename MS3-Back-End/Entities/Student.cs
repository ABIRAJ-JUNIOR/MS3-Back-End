using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS3_Back_End.Entities
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        public string Nic { get; set; } = string.Empty;
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
        public Address? address { get; set; }
        public ICollection<Enrollment>? enrollments { get; set; }
        public ICollection<Notification>? notifications { get; set; }
        public ICollection<Feedbacks>? feedbacks { get; set; }
        public ICollection<StudentAssesment>? assesments { get; set; }
    }
}
