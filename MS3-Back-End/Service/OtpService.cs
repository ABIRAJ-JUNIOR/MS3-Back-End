using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using Org.BouncyCastle.Bcpg.OpenPgp;
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

        public async Task<string> OtpVerification(verifyOtp verifyDetails)
        {
            var data = await _repository.CheckOtpVerification(verifyDetails);
            if (data == null)
            {
               
                if (verifyDetails.Otp == data.Otpdata)
                {
                    if ((DateTime.Now - data.OtpGenerated).TotalMinutes > 5)
                    {
                        await _repository.DeleteOtpDetails(data);
                        return "OTP expired";
                    }

                    data.IsUsed = true;
                   await _repository.DeleteOtpDetails(data);
                    return "OtpVerified SuccesFull";
                }
                else
                {
                    throw new Exception("Otp is invalid");

                }
            }else
            {
                return "Otp verified invalid";
            }       
        }

    }
}
