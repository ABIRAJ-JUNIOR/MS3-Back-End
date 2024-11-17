using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbacksService feedbackService;

        public FeedbacksController(IFeedbacksService feedbackService)
        {
            this.feedbackService = feedbackService;
        }
        [HttpPost]
    }
}
