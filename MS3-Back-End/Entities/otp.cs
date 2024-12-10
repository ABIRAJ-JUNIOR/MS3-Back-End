namespace MS3_Back_End.Entities
{
    public class otp
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public  DateTime OtpGenerated{ get; set; }
        public  DateTime OtpExpire{ get; set; }
        public  bool IsUsed { get; set; }
        public User User { get; set; }
    }
}
