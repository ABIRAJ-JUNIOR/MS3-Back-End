using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Enrollment
{
    public class EnrollmentResponseDTO
    {
        public Guid Id { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public bool IsActive { get; set; }

        public Guid StudentId { get; set; }
        public Guid CourseSheduleId { get; set; }
    }
}
