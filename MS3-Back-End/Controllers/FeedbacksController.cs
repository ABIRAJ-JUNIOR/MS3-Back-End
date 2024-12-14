using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [HttpGet("Student/{Id}")]
        public async Task<IActionResult> GetFeedBacksByStudentId(Guid Id)
        {
            try
            {
                var data = await _feedbackService.GetFeedBacksByStudentId(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult>  GetPaginatedFeedBack(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _feedbackService.GetPaginatedFeedBack(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
