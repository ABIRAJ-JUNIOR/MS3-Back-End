using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("StudentAssessments")]
        public async  Task<IActionResult> GetAllAssessments()
        {
            var assessmentList = await _service.GetAllAssessments();
            return Ok(assessmentList);
        }
    }
}
