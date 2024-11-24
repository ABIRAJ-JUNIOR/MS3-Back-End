using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentMethots PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public int? InstallmentNumber { get; set; }

        public Guid EnrollmentId { get; set; }

        //Reference
        public Enrollment? Enrollment { get; set; }
    }

    public enum PaymentTypes
    {
        FullPayment = 1,
        Installment = 2
    }

    public enum PaymentMethots
    {
        Card = 1,
        BankTransfer = 2,
        OnlineBanking = 3,
        Cash = 4
    }
}
