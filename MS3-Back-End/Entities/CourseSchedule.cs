using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class CourseSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int Duration { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }

        public Guid CourseId { get; set; }

        //Reference
        public Course? Course { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

    }

    public enum ScheduleStatus
    {
        Open = 1,
        Closed = 2,
        Completed = 3
    }
}
