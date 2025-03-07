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
        private readonly ILogger<FeedbacksController> _logger;

        public FeedbacksController(IFeedbacksService feedbackService, ILogger<FeedbacksController> logger)
        {
            _feedbackService = feedbackService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FeedbacksResponceDTO>> AddFeedback(FeedbacksRequestDTO feedbacksRequestDTO)
        {
            if (feedbacksRequestDTO == null)
            {
                return BadRequest("Feedback data is required.");
            }

            try
            {
                var data = await _feedbackService.AddFeedbacks(feedbacksRequestDTO);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding feedback");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FeedBacks")]
        public async Task<ActionResult<IEnumerable<FeedbacksResponceDTO>>> GetAllFeedbacks()
        {
            try
            {
                var data = await _feedbackService.GetAllFeedbacks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all feedbacks");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TopFeedBacks")]
        public async Task<ActionResult<IEnumerable<FeedbacksResponceDTO>>> GetTopFeedbacks()
        {
            try
            {
                var data = await _feedbackService.GetTopFeetbacks();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top feedbacks");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Student/{id}")]
        public async Task<ActionResult<IEnumerable<FeedbacksResponceDTO>>> GetFeedbacksByStudentId(Guid id)
        {
            try
            {
                var data = await _feedbackService.GetFeedBacksByStudentId(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting feedbacks by student id {id}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<PaginatedFeedbackResponseDTO>>> GetPaginatedFeedback(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _feedbackService.GetPaginatedFeedBack(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated feedbacks");
                return BadRequest(ex.Message);
            }
        }
    }
}
