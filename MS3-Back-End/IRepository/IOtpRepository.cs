using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IOtpRepository
    {
        Task<bool> emailVerification(GenerateOtp otpDetail);
        Task<string> SaveGeneratedOtp(otp otpRequest);
    }
}
