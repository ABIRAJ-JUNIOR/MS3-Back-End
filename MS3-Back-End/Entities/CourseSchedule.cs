using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class CourseSchedule
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; private set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int Duration { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }

        //Reference
        public Course? course { get; set; }
        public ICollection<Enrollment>? enrollments { get; set; }

    }

    public enum ScheduleStatus
    {
        Open = 1,
        Closed = 2,
        Completed = 3
    }
}
