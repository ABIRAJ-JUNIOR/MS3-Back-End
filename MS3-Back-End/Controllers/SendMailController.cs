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
        public async Task<IActionResult> OtpMail(SendOtpMailRequest sendMailRequest)
        {
            var res = await _sendMailService.OtpMail(sendMailRequest).ConfigureAwait(false);
            return Ok(res);
        }

        [HttpPost("Invoice")]
        public async Task<IActionResult> InvoiceMail(SendInvoiceMailRequest sendMailRequest)
        {
            var res = await _sendMailService.InvoiceMail(sendMailRequest).ConfigureAwait(false);
            return Ok(res);
        }

    }
}
