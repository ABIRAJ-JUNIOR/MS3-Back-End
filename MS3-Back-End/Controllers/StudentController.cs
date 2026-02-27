using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.password_student;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.IService;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize]
        [HttpPost("student")]
        public async Task<ActionResult<StudentResponseDTO>> AddStudent(StudentRequestDTO studentRequest)
        {
            if (studentRequest == null)
            {
                return BadRequest("Student data is required");
            }

            try
            {
                var studentResponse = await _studentService.AddStudent(studentRequest);
                return Ok(studentResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding student");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<StudentResponseDTO>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudent();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all students");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentFullDetailsResponseDTO>> GetStudentFullDetailsById(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentFullDetailsById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting student full details by id {id}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update-Full-Details/{id}")]
        public async Task<ActionResult<StudentResponseDTO>> UpdateStudentFullDetails(Guid id, StudentFullUpdateDTO request)
        {
            if (request == null)
            {
                return BadRequest("Update data is required");
            }

            try
            {
                var updatedData = await _studentService.UpdateStudentFullDetails(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating student full details with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update-Personal-Details")]
        public async Task<ActionResult<StudentResponseDTO>> UpdateStudent(StudentUpdateDTO studentUpdate)
        {
            if (studentUpdate == null)
            {
                return BadRequest("Student data is required");
            }

            try
            {
                var updatedStudent = await _studentService.UpdateStudent(studentUpdate);
                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating student");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeleteStudent(Guid id)
        {
            try
            {
                var result = await _studentService.DeleteStudent(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting student with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Image/{studentId}")]
        public async Task<ActionResult<string>> UploadImage(Guid studentId, IFormFile? image)
        {
            try
            {
                var response = await _studentService.UploadImage(studentId, image);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error uploading image for student id {studentId}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<StudentWithUserResponseDTO>>> GetStudentByPagination(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _studentService.GetPaginatedStudent(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated students");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update-info-Details/{id}")]
        public async Task<ActionResult<StudentResponseDTO>> UpdateStudentInfoDetails(Guid id, StudentFullUpdateDTO request)
        {
            if (request == null)
            {
                return BadRequest("Update data is required");
            }

            try
            {
                var updatedData = await _studentService.UpdateStudentInfoDetails(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating student info details with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("changeStudentPassword/{id}")]
        public async Task<ActionResult<string>> UpdateStudentPassword(Guid id, PasswordRequest auth)
        {
            if (auth == null)
            {
                return BadRequest("Password data is required");
            }

            try
            {
                var updatedData = await _studentService.UpdateStudentPassword(id, auth);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error changing student password for id {id}");
                return BadRequest(ex.Message);
            }
        }
    }
}
