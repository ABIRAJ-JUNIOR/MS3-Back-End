using MS3_Back_End.DTOs.Email;
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
        private readonly IAuthRepository _Authrepository;
        private readonly SendMailService _sendmailservice;
        private readonly IStudentRepository _studentrepository;

        public OtpService(IOtpRepository repository, IAuthRepository authrepository, SendMailService sendmailservice, IStudentRepository studentrepository)
        {
            _repository = repository;
            _Authrepository = authrepository;
            _sendmailservice = sendmailservice;
            _studentrepository = studentrepository;
        }

        public async Task<string> EmailVerification(GenerateOtp otpDetails)
        {
            var response = await _repository.EmailVerification(otpDetails);
            var studentData = await _studentrepository.GetStudentById(response.Id);

            if(studentData.IsActive == false)
            {
                throw new Exception("InActive Account");
            }

            Random random = new Random();
            var otpObject = new Otp
            {
                UserId = response.Id,
                Email = otpDetails.Email,
                Otpdata = Convert.ToString(random.Next(1000, 10000)),
                OtpGenerated = DateTime.Now,
            };

            var responseData = await _repository.SaveGeneratedOtp(otpObject);


            var Otp = new SendOtpMailRequest()
            {
                Name =  studentData.FirstName + " " + studentData.LastName,
                Otp = otpObject.Otpdata,
                Email = otpDetails.Email,
                EmailType = EmailTypes.ResetPasswordOTP,
            };

            await _sendmailservice.OtpMail(Otp);

            return responseData;

        }

        public async Task<string> OtpVerification(verifyOtp verifyDetails)
        {
            var data = await _repository.CheckOtpVerification(verifyDetails);
            if (data != null)
            {
               
                if (verifyDetails.Otp == data.Otpdata)
                {
                    if ((DateTime.Now - data.OtpGenerated).TotalMinutes > 5)
                    {
                        await _repository.DeleteOtpDetails(data);
                        throw new Exception("OTP expired");
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
                throw new Exception("Otp verified invalid");
            }       
        }

        public async Task<string> ChangePassword(ChangePassword otpDetails)
        {
            var response = await _Authrepository.GetUserByEmail(otpDetails.Email);
            response.Password = BCrypt.Net.BCrypt.HashPassword(otpDetails.NewPassword);
            var data = await _Authrepository.UpdateUser(response);
            return "Password  Changed Succesfully.";
        }
    }
}
