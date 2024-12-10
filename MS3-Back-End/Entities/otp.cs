namespace MS3_Back_End.Entities
{
    public class Otp
    {
        public Guid Id { get; set; }
        // Foreign Key
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Otpdata { get; set; }
        public  DateTime OtpGenerated{ get; set; } = DateTime.Now;
        public  bool IsUsed { get; set; } = false;
        public User User { get; set; } 
    }
}
