namespace MS3_Back_End.DTOs.Email
{
    public class EmailConfig
    {
        public string FromName { get; set; } = string.Empty;
        public string FromAddess { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
