namespace MS3_Back_End.Entities
{
    public class Otp
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;   
        public string Otpdata { get; set; } = string.Empty;
        public DateTime OtpGenerated { get; set; } = DateTime.Now;
        public bool IsUsed { get; set; } = false;
        public User? User { get; set; } 
    }
}
