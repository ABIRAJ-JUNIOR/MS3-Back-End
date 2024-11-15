using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSheduleController : ControllerBase
    {
        private readonly ICourseSheduleService _courseScheduleService;

        public CourseSheduleController(ICourseSheduleService courseScheduleService)
        {
            _courseScheduleService = courseScheduleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourseSchedule(CourseSheduleRequestDTO courseReq)
        {
            if (courseReq == null)
            {
                return BadRequest("Course schedule data is required.");
            }
            try
            {

                var response = await _courseScheduleService.AddCourseShedule(courseReq);
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CourseShedule/{id}")]
        public async Task<IActionResult> GetCourseSheduleById(Guid id)
        {
            try
            {
                var response = await _courseScheduleService.GetCourseSheduleById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourseShedule()
        {
            try
            {
                var response = await _courseScheduleService.GetAllCourseShedule();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("searchByLocation")]
        public async Task<IActionResult> SearchCourseShedule( string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return BadRequest("Search text is required.");
            }

            try
            {
                var response = await _courseScheduleService.SearchCourseShedule(searchText);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("courseId/{CourseId}")]
        public async Task<IActionResult> UpdateCourseShedule(UpdateCourseSheduleDTO courseReq)
        {
            try
            {
                var response = await _courseScheduleService.UpdateCourseShedule(courseReq);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
