using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Student;
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
        public async Task<IActionResult> AddStudent([FromBody] StudentRequestDTO studentRequest)
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
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
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
       
    }
}
