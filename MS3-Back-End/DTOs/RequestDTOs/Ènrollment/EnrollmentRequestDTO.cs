using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Ènrollment
{
    public class EnrollmentRequestDTO
    {
        public DateOnly EnrollmentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public bool IsActive { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseSheduleId { get; set; }
    }
}
