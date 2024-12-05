namespace MS3_Back_End.DTOs.Email
{
    public class MailModel
    {
        public string To { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
