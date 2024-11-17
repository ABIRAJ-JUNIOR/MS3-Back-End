using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _service;

        public AssessmentController(IAssessmentService service)
        {
            _service = service;
        }


        [HttpPost("Assessment")]
        public async Task<IActionResult> AddAssessment(AssessmentRequestDTO request)
        {
            try
            {
                var assessmentData = await _service.AddAssessment(request);
                return Ok(assessmentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Assessments")]
        public async Task<IActionResult> GetAllAssessment()
        {
            var assessmentList = await _service.GetAllAssessment();
            return Ok(assessmentList);
        }

        [HttpPut("Assessment/{id}")]
        public async Task<IActionResult> UpdateAssessment(Guid id, UpdateAssessmentRequestDTO request)
        {
            try
            {
                var updatedData = await _service.UpdateAssessment(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
