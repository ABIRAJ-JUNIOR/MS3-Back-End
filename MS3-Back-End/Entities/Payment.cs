using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EnrollementId { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public PaymentMethots PaymentMethod { get; set; }
        public decimal AmmountPaid { get; set; }
        public DateOnly PaymentDate { get; set; }
        public int InstallmentNumber { get; set; }

        //Reference
        public Enrollment? Enrollment { get; set; }
    }

    public enum PaymentTypes
    {
        FullPayment=1,
        Installment=2
    }

    public enum PaymentMethots
    {
        Card=1,
        Online=2,
        Bank=3,
        Paypal=4
    }
}
