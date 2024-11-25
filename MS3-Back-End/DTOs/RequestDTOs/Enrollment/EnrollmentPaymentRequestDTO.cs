using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Enrollment
{
    public class EnrollmentPaymentRequestDTO
    {
        public PaymentTypes PaymentType { get; set; }
        public PaymentMethots PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public int? InstallmentNumber { get; set; }
    }
}
