using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpPost("student")]
        public async Task<IActionResult> AddStudent(StudentRequestDTO studentRequest)
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudent();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentFullDetailsById(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentFullDetailsById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Full-Details/{id}")]
        public async Task<IActionResult> UpdateStudentFullDetails(Guid id, StudentFullUpdateDTO request)
        {
            try
            {
                var updatedData = await _studentService.UpdateStudentFullDetails(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Personal-Details")]
        public async Task<IActionResult> UpdateStudent(StudentUpdateDTO studentUpdate)
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
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                var result = await _studentService.DeleteStudent(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Image/{studentId}")]
        public async Task<IActionResult> UploadImage(Guid studentId, IFormFile? image)
        {
            try
            {
                var response = await _studentService.UploadImage(studentId, image);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetStudentByPagination(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _studentService.GetPaginatedStudent(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
