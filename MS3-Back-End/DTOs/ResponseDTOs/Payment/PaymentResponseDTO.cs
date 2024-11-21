using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Payment
{
    public class PaymentResponseDTO
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ImagePath { get; set; }
        public int? InstallmentNumber { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
