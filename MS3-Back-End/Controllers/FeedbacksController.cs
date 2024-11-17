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
        [HttpPost("Add-Feedbacks")]
        public async Task<IActionResult> AddFeedback(FeedbacksRequestDTO feedbacksRequestDTO) 
        {
            try
            {
                var data = await _feedbackService.AddFeedbacks(feedbacksRequestDTO);
                return Ok(data);
            }
            catch (Exception ex) 
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
