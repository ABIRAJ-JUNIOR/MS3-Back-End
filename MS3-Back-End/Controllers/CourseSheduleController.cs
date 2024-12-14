using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseScheduleController : ControllerBase
    {
        private readonly ICourseScheduleService _courseScheduleService;

        public CourseScheduleController(ICourseScheduleService courseScheduleService)
        {
            _courseScheduleService = courseScheduleService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCourseSchedule(CourseScheduleRequestDTO courseReq)
        {
            try
            {
                var response = await _courseScheduleService.AddCourseSchedule(courseReq);
                return Ok(response);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CourseSchedule/{id}")]
        public async Task<IActionResult> GetCourseScheduleById(Guid id)
        {
            try
            {
                var response = await _courseScheduleService.GetCourseScheduleById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCourseSchedule()
        {
            try
            {
                var response = await _courseScheduleService.GetAllCourseSchedule();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("searchByLocation")]
        public async Task<IActionResult> SearchCourseSchedule( string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return BadRequest("Search text is required.");
            }

            try
            {
                var response = await _courseScheduleService.SearchCourseSchedule(searchText);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCourseSchedule(Guid id, UpdateCourseScheduleDTO courseReq)
        {
            try
            {
                var response = await _courseScheduleService.UpdateCourseSchedule(id,courseReq);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedCoursesSchedules(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _courseScheduleService.GetPaginatedCoursesSchedules(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
