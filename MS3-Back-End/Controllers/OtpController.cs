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
        public OtpController(IOtpService service)
        {
            _service = service;
        }

        [HttpPost("emailVerfication")]
        public async Task<IActionResult> verifyEmail(GenerateOtp otpDetailDetails)
        {
            try
            {

                var data = await _service.EmailVerification(otpDetailDetails);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}