using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AssessmentController(IAssessmentService service)
        {
            _service = service;
        }


        [HttpPost("Add")]
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAssessment()
        {
            var assessmentList = await _service.GetAllAssessment();
            return Ok(assessmentList);
        }

        [HttpPut("Update/{id}")]
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

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedAssessment(int pageNumber, int pageSize)
        {
            try
            {
                var response = await _service.GetPaginatedAssessment(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
