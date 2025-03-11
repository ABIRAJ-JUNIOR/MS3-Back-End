using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.IService;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResponseDTO>> CreatePayment(PaymentRequestDTO paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Payment data is required.");
            }

            try
            {
                var paymentResponse = await _paymentService.CreatePayment(paymentRequest);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating payment");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PaymentResponseDTO>>> GetAllPayments()
        {
            try
            {
                var paymentsList = await _paymentService.GetAllPayments();
                return Ok(paymentsList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all payments");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Recent")]
        public async Task<ActionResult<IEnumerable<PaymentResponseDTO>>> RecentPayments()
        {
            try
            {
                var recentPayments = await _paymentService.RecentPayments();
                return Ok(recentPayments);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting recent payments");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PaymentReminder")]
        public async Task<ActionResult<string>> PaymentReminderSend()
        {
            try
            {
                var response = await _paymentService.PaymentReminderSend();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error sending payment reminder");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PaymentOverview")]
        public async Task<ActionResult<PaymentOverview>> GetPaymentOverview()
        {
            try
            {
                var paymentOverview = await _paymentService.GetPaymentOverview();
                return Ok(paymentOverview);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting payment overview");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<PaymentFullDetails>>> GetPaginatedPayments(int pageNumber, int pageSize)
        {
            try
            {
                var response = await _paymentService.GetPaginatedPayments(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated payments");
                return BadRequest(ex.Message);
            }
        }
    }
}
