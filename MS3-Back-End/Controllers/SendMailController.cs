using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Email;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly SendMailService _sendMailService;

        public SendMailController(SendMailService sendmailService)
        {
            _sendMailService = sendmailService;
        }

        [HttpPost("OTP")]
        public async Task<IActionResult> Sendmail(SendMailRequest sendMailRequest)
        {
            var res = await _sendMailService.Sendmail(sendMailRequest).ConfigureAwait(false);
            return Ok(res);
        }

    }
}
