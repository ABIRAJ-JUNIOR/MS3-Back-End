using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;
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

        [HttpGet("FeedBacks")]
        public async Task<IActionResult> getAllFeedbacks()
        {
            try
            {
                var data = await _feedbackService.GetAllFeedbacks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TopFeedBacks")]
        public async Task<IActionResult> GetTopFeetbacks()
        {
            try
            {
                var data = await _feedbackService.GetTopFeetbacks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Students")]

        public async Task<IActionResult> GetFeedBacksBySrudentId(Guid Id)
        {
            try
            {
                var data = await _feedbackService.GetFeedBacksBySrudentId(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
