namespace MS3_Back_End.DTOs.ResponseDTOs.Payment
{
    public class PaymentFullDetails
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseName {  get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime TransactionDate {  get; set; }
        public DateTime? DueDate { get; set; }
        public bool isReminder { get; set; }

    }
}
