namespace MS3_Back_End.DTOs.Email
{
    public class SendMessageMailRequest
    {
        public string Name { get; set; } = string.Empty;
        public string UserMessage { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EmailTypes EmailType { get; set; }
    }
}
