namespace MS3_Back_End.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public EmailTypes emailTypes { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}

public enum EmailTypes
{
    None = 0,
    Otp,
    Invoice,
    Message,
    Response,
    EmailVerification
}