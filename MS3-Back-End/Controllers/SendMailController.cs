using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Email;
using MS3_Back_End.Service;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly SendMailService _sendMailService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SendMailController(SendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [HttpPost("OTP")]
        public async Task<ActionResult<string>> OtpMail(SendOtpMailRequest sendMailRequest)
        {
            if (sendMailRequest == null)
            {
                return BadRequest("OTP mail request data is required.");
            }

            try
            {
                var res = await _sendMailService.OtpMail(sendMailRequest).ConfigureAwait(false);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error sending OTP mail");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Invoice")]
        public async Task<ActionResult<string>> InvoiceMail(SendInvoiceMailRequest sendMailRequest)
        {
            if (sendMailRequest == null)
            {
                return BadRequest("Invoice mail request data is required.");
            }

            try
            {
                var res = await _sendMailService.InvoiceMail(sendMailRequest).ConfigureAwait(false);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error sending invoice mail");
                return BadRequest(ex.Message);
            }
        }
    }
}
