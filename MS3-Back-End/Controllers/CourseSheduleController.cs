using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseScheduleController : ControllerBase
    {
        private readonly ICourseScheduleService _courseScheduleService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CourseScheduleController(ICourseScheduleService courseScheduleService)
        {
            _courseScheduleService = courseScheduleService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CourseScheduleResponseDTO>> AddCourseSchedule(CourseScheduleRequestDTO courseReq)
        {
            if (courseReq == null)
            {
                return BadRequest("Course schedule data is required.");
            }

            try
            {
                var response = await _courseScheduleService.AddCourseSchedule(courseReq);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding course schedule");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CourseSchedule/{id}")]
        public async Task<ActionResult<CourseScheduleResponseDTO>> GetCourseScheduleById(Guid id)
        {
            try
            {
                var response = await _courseScheduleService.GetCourseScheduleById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting course schedule by id {id}");
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CourseScheduleResponseDTO>>> GetAllCourseSchedules()
        {
            try
            {
                var response = await _courseScheduleService.GetAllCourseSchedule();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all course schedules");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("searchByLocation")]
        public async Task<ActionResult<IEnumerable<CourseScheduleResponseDTO>>> SearchCourseSchedule(string searchText)
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
                _logger.Error(ex, "Error searching course schedules");
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CourseScheduleResponseDTO>> UpdateCourseSchedule(Guid id, UpdateCourseScheduleDTO courseReq)
        {
            if (courseReq == null)
            {
                return BadRequest("Update data is required.");
            }

            try
            {
                var response = await _courseScheduleService.UpdateCourseSchedule(id, courseReq);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating course schedule with id {id}");
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<CourseScheduleResponseDTO>>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _courseScheduleService.GetPaginatedCoursesSchedules(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated course schedules");
                return BadRequest(ex.Message);
            }
        }
    }
}
