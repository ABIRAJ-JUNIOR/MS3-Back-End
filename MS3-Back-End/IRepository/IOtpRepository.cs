using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IOtpRepository
    {
        Task<User> EmailVerification(GenerateOtp otpDetail);
        Task<string> SaveGeneratedOtp(Otp otpRequest);
        Task<Otp> CheckOtpVerification(verifyOtp otpDetail);
        Task<string> DeleteOtpDetails(Otp OtpDetails);

    }
}
