using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment( PaymentRequestDTO paymentRequest)
        {
            try
            {
                var paymentResponse = await _paymentService.CreatePayment(paymentRequest);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPayments()
        {
            var paymentsList = await _paymentService.GetAllPayments();
            return Ok(paymentsList);
        }

        [HttpGet("Recent")]
        public async Task<IActionResult> RecentPayments()
        {
            var recentPayments = await _paymentService.RecentPayments();
            return Ok(recentPayments);
        }

        [HttpGet("PaymentReminder")]
        public async Task<IActionResult> PaymentReminderSend()
        {
            var response = await _paymentService.PaymentReminderSend();
            return Ok(response);
        }

        [HttpGet("PaymentOverView")]
        public async Task<IActionResult> GetPaymentOverview()
        {
            var paymentOverview = await _paymentService.GetPaymentOverview();
            return Ok(paymentOverview);
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]

        public async Task<IActionResult> GetPaginatedPayments(int pageNumber, int pageSize)
        {
            var response = await _paymentService.GetPaginatedPayments(pageNumber, pageSize);
            return Ok(response);
        }
    }
}
