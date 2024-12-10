namespace MS3_Back_End.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public EmailTypes emailTypes { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}

public enum EmailTypes
{
    None = 0,
    PaymentOtp,
    Invoice,
    Message,
    Response,
    EmailVerification,
    ResetPasswordOTP
    
}