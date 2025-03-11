using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollementService _enrollmentService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EnrollmentController(IEnrollementService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDTO>> AddEnrollment(EnrollmentRequestDTO enrollmentReq)
        {
            if (enrollmentReq == null)
            {
                return BadRequest("Enrollment data is required.");
            }

            try
            {
                var enrollment = await _enrollmentService.AddEnrollment(enrollmentReq);
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding enrollment");
                return BadRequest(ex.Message);
            }
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
                _logger.Error(ex, $"Error getting enrollment by id {id}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Enrollments/{studentId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentResponseDTO>>> GetEnrollmentsByStudentId(Guid studentId)
        {
            try
            {
                var enrollments = await _enrollmentService.GetEnrollmentsByStudentId(studentId);
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting enrollments by student id {studentId}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentResponseDTO>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollements();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all enrollments");
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
                _logger.Error(ex, $"Error deleting enrollment with id {id}");
                return NotFound(ex.Message);
            }
        }
    }
}
