
using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDBContext _Db;
        public OtpRepository(AppDBContext db)
        {
            _Db = db;
        }

        public async Task<User> EmailVerification(GenerateOtp otpDetail)
        {
            var responseData = await _Db.Users.Include(ur => ur.UserRole).ThenInclude(r => r.Role).SingleOrDefaultAsync(user => user.Email == otpDetail.Email);
            return responseData!;
        }

        public async Task<string> SaveGeneratedOtp(Otp otpRequest)
        {
            var ResponseData = await _Db.Otps.AddAsync(otpRequest);
            await _Db.SaveChangesAsync();
            return "Email Verfication SuccesFully.";

        }
        public async Task<Otp> CheckOtpVerification(verifyOtp otpDetail)
        {
            var ResponseData = await _Db.Otps.Where(a=>a.IsUsed==false).SingleOrDefaultAsync(otp=>otp.Email== otpDetail.Email && otp.Otpdata == otpDetail.Otp);
            return ResponseData!;
        }
        public async Task<string> DeleteOtpDetails(Otp OtpDetails)
        {
            _Db.Otps.Update(OtpDetails);
            await _Db.SaveChangesAsync();
            return "Otp Deleted Successfully.";
        }
    }
}
