using MS3_Back_End.DTOs.Otp;

namespace MS3_Back_End.IRepository
{
    public interface IOtpRepository
    {
        Task<bool> emailVerification(GenerateOtp otpDetail);
    }
}
