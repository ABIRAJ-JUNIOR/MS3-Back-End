
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

        public async Task<bool> emailVerification(GenerateOtp otpDetail)
        {
            var responseData = await _Db.Users.SingleOrDefaultAsync(user=>user.Email==otpDetail.Email);
            if (responseData != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
