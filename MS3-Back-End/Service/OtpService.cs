using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using System;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MS3_Back_End.Service
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _repository;
        public OtpService(IOtpRepository repo)
        {
            _repository = repo;
        }

        public async Task<string> EmailVerification(GenerateOtp otpDetails)
        {
            var response = await _repository.EmailVerification(otpDetails);
                Random random = new Random();

                var otpObject = new Otp
                {
                    UserId = response.Id,
                    Email = otpDetails.Email,
                    Otpdata = Convert.ToString(random.Next(1000, 10000)),
                    OtpGenerated = DateTime.Now,
                };
                var responseData = await _repository.SaveGeneratedOtp(otpObject);
                return responseData;
            
            

        }

    }
}
