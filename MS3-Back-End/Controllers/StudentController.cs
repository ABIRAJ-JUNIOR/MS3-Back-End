using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.DTO.ResponseDTOs;
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

        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("get-student-by-nic/{nic}")]
        public async Task<IActionResult> GetStudentByNic(string nic)
        {
            try
            {
                var student = await _studentService.GetStudentByNic(nic);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("add-student")]
        public async Task<IActionResult> AddStudent( StudentRequestDTO studentRequest)
        {
            try
            {
                var studentDetails = await _studentService.AddStudent(studentRequest);
                return Ok(studentDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
