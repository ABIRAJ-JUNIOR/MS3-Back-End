using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentAssessmentController : ControllerBase
    {
        private readonly IStudentAssessmentService _service;
        private readonly ILogger<StudentAssessmentController> _logger;

        public StudentAssessmentController(IStudentAssessmentService studentAssessmentService, ILogger<StudentAssessmentController> logger)
        {
            _service = studentAssessmentService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<StudentAssessmentResponseDTO>>> GetAllAssessments()
        {
            try
            {
                var assessmentList = await _service.GetAllAssessments();
                return Ok(assessmentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all assessments");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Evaluated")]
        public async Task<ActionResult<IEnumerable<StudentAssessmentResponseDTO>>> GetAllEvaluatedAssessments()
        {
            try
            {
                var assessmentList = await _service.GetAllEvaluatedAssessments();
                return Ok(assessmentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all evaluated assessments");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Non-Evaluate")]
        public async Task<ActionResult<IEnumerable<StudentAssessmentResponseDTO>>> GetAllNonEvaluateAssessments()
        {
            try
            {
                var assessmentList = await _service.GetAllNonEvaluateAssessments();
                return Ok(assessmentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all non-evaluated assessments");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddStudentAssessment(StudentAssessmentRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Student assessment data is required.");
            }

            try
            {
                var studentAssessmentData = await _service.AddStudentAssessment(request);
                return Ok(studentAssessmentData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student assessment");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("studentAssessment/{id}")]
        public async Task<ActionResult<StudentAssessmentResponseDTO>> GetStudentAssessmentById(Guid id)
        {
            try
            {
                var assessment = await _service.GetStudentAssesmentById(id);
                return Ok(assessment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting student assessment by id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Evaluate-Assessment/{id}")]
        public async Task<ActionResult<StudentAssessmentResponseDTO>> EvaluateStudentAssessment(Guid id, EvaluationRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Evaluation data is required.");
            }

            try
            {
                var evaluateAssessment = await _service.EvaluateStudentAssessment(id, request);
                return Ok(evaluateAssessment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error evaluating student assessment with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getByPagination/{studentId}")]
        public async Task<ActionResult<PaginationResponseDTO<StudentAssessmentResponseDTO>>> GetPaginationByStudentId(Guid studentId, int pageNumber, int pageSize)
        {
            try
            {
                var response = await _service.PaginationGetByStudentID(studentId, pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting paginated assessments for student id {studentId}");
                return BadRequest(ex.Message);
            }
        }
    }
}
