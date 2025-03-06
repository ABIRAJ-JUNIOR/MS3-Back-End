using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _service;
        private readonly ILogger<AssessmentController> _logger;

        public AssessmentController(IAssessmentService service, ILogger<AssessmentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<AssessmentResponseDTO>> AddAssessment(AssessmentRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Assessment data is required.");
            }

            try
            {
                var assessmentData = await _service.AddAssessment(request);
                return Ok(assessmentData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding assessment");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AssessmentResponseDTO>>> GetAllAssessments()
        {
            try
            {
                var assessmentList = await _service.GetAllAssessment();
                return Ok(assessmentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all assessments");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<AssessmentResponseDTO>> UpdateAssessment(Guid id, UpdateAssessmentRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Update data is required.");
            }

            try
            {
                var updatedData = await _service.UpdateAssessment(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating assessment with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<AssessmentResponseDTO>>> GetPaginatedAssessment(int pageNumber, int pageSize)
        {
            try
            {
                var response = await _service.GetPaginatedAssessment(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated assessments");
                return BadRequest(ex.Message);
            }
        }
    }

}
