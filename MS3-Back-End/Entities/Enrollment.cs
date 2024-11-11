namespace MS3_Back_End.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public Guid CourseSheduleId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public bool IsActive { get; set; }
        public Guid StudentId { get; set; }

        //Reference
        public Student? Student { get; set; }
        public CourseSchedule? CourseSchedules { get; set; }
        public ICollection<Payment>? Payments { get; set; }

    }
    public enum PaymentStatus
    {
        Paid =1,
        InProcess=2
    }   
}

