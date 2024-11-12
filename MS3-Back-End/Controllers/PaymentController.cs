using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


    }
}
