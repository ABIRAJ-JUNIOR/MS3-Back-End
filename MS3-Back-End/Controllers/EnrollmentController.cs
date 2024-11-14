using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollementService _enrollmentService;
        public EnrollmentController(IEnrollementService enrollement)
        {
            _enrollmentService = enrollement;
        }


        [HttpGet("Enrollment/{id}")]
        public async Task<ActionResult<EnrollmentResponseDTO>> GetEnrollmentById(Guid id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentId(id);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> SearchEnrollmentByUserId(Guid userId)
        {
            try
            {
                var enrollments = await _enrollmentService.SearchEnrollmentByUserId(userId);
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Enrollments")]
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollements();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDTO>> AddEnrollment([FromBody] EnrollmentRequestDTO enrollmentReq)
        {
            try
            {
                var enrollment = await _enrollmentService.AddEnrollment(enrollmentReq);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentResponseDTO>> UpdateEnrollment(Guid id, [FromBody] EnrollmentUpdateDTO enrollment)
        {
            if (id != enrollment.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                var updatedEnrollment = await _enrollmentService.UpdateEnrollment(enrollment);
                return Ok(updatedEnrollment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteEnrollment(Guid id)
        {
            try
            {
                var result = await _enrollmentService.DeleteEnrollment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }




    }
}
