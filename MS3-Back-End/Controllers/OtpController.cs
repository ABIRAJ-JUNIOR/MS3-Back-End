﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Otp;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _service;
        private readonly ILogger<OtpController> _logger;

        public OtpController(IOtpService service, ILogger<OtpController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("emailVerification")]
        public async Task<ActionResult<string>> VerifyEmail(GenerateOtp otpDetails)
        {
            if (otpDetails == null)
            {
                return BadRequest("OTP details are required.");
            }

            try
            {
                var data = await _service.EmailVerification(otpDetails);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying email");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("otpVerification")]
        public async Task<ActionResult<string>> VerifyOtp(verifyOtp otpDetails)
        {
            if (otpDetails == null)
            {
                return BadRequest("OTP details are required.");
            }

            try
            {
                var data = await _service.OtpVerification(otpDetails);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying OTP");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("changePassword")]
        public async Task<ActionResult<string>> ChangeUserPassword(ChangePassword changePasswordDetails)
        {
            if (changePasswordDetails == null)
            {
                return BadRequest("Change password details are required.");
            }

            try
            {
                var data = await _service.ChangePassword(changePasswordDetails);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing user password");
                return BadRequest(ex.Message);
            }
        }
    }
}
