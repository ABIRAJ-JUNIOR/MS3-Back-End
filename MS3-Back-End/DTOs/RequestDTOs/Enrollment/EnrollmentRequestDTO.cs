using MS3_Back_End.DTOs.RequestDTOs.Enrollment;
using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Ènrollment
{
    public class EnrollmentRequestDTO
    {
        public Guid StudentId { get; set; }
        public Guid CourseSheduleId { get; set; }

        public EnrollmentPaymentRequestDTO PaymentRequest { get; set; } = new EnrollmentPaymentRequestDTO();
    }
}
