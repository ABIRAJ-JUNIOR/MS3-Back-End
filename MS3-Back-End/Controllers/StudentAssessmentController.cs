using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAssessmentController : ControllerBase
    {
        private readonly IStudentAssessmentService _service;

        public StudentAssessmentController(IStudentAssessmentService studentAssessmentService)
        {
            _service = studentAssessmentService;
        }

        [HttpGet("GetAll")]
        public async  Task<IActionResult> GetAllAssessments()
        {
            var assessmentList = await _service.GetAllAssessments();
            return Ok(assessmentList);
        }

        [HttpGet("Evaluated")]
        public async Task<IActionResult> GetAllEvaluatedAssessments()
        {
            var assessmentList = await _service.GetAllEvaluatedAssessments();
            return Ok(assessmentList);
        }

        [HttpGet("Non-Evaluate")]
        public async Task<IActionResult> GetAllNonEvaluateAssessments()
        {
            var assessmentList = await _service.GetAllNonEvaluateAssessments();
            return Ok(assessmentList);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentAssessment(StudentAssessmentRequestDTO request)
        {
            var studentAssessmentData = await _service.AddStudentAssessment(request);
            return Ok(studentAssessmentData);
        }
        [HttpGet("studentAssesment/{id}")]
        public async Task<IActionResult> GetAllNonEvaluateAssessments(Guid id)
        {
            var assessmentList = await _service.GetStudentAssesmentById(id);
            return Ok(assessmentList);
        }

        [HttpPut("Evaluate-Assessment/{id}")]
        public async Task<IActionResult> EvaluateStudentAssessment(Guid id, EvaluationRequestDTO request)
        {
            try
            {
                var evaluateAssessment = await _service.EvaluateStudentAssessment(id, request);
                return Ok(evaluateAssessment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getByPagination${studentId}")]
        public async Task<IActionResult> GetPaginationByStudentId(Guid studentId, int PageNumber, int PageSize)
        {
            try
            {
                var response = await _service.PaginationGetByStudentID(studentId, PageNumber, PageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
