namespace MS3_Back_End.DTOs.Email
{
    public class SendInvoiceMailRequest
    {
        public Guid InvoiceId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string CourseName {  get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public EmailTypes EmailType { get; set; }
    }
}
