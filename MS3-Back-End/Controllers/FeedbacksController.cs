using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbacksService _feedbackService;

        public FeedbacksController(IFeedbacksService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpPost]
        public async Task<IActionResult> AddFeedback(FeedbacksRequestDTO feedbacksRequestDTO) 
        {
            var data=await _feedbackService.AddFeedbacks(feedbacksRequestDTO);
            return Ok(data);
        }
    }
}
