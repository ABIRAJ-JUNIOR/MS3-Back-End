namespace MS3_Back_End.DTOs.Email
{
    public class SendResponseMailRequest
    {
        public string Name { get; set; } = string.Empty;
        public string AdminResponse { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EmailTypes EmailType { get; set; }
    }
}
