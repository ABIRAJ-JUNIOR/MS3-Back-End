namespace MS3_Back_End.DTOs.Email
{
    public class SendMailRequest
    {
        public string? Name { get; set; } 
        public string? Otp { get; set; }
        public string? Email { get; set; }
        public EmailTypes EmailType { get; set; }
    }
}
