using MS3_Back_End.DTOs.Otp;

namespace MS3_Back_End.IService
{
    public interface IOtpService
    {
        Task<string> EmailVerification(GenerateOtp otpDetails);
        Task<string> OtpVerification(verifyOtp verifyDetails);
    }
}
