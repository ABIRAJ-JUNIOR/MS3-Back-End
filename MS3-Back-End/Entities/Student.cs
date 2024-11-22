using System.ComponentModel.DataAnnotations;

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
        public Gender Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;


        //Reference
        public Address? Address { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Feedbacks>? Feedbacks { get; set; }
        public ICollection<StudentAssessment>? StudentAssessments { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}
