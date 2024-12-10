using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.Otp
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Otp { get; set; }
    }
}
