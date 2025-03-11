using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using NLog;

namespace MS3_Back_End.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<CourseResponseDTO>> AddCourse(CourseRequestDTO courseRequest)
        {
            if (courseRequest == null)
            {
                return BadRequest("Course data is required.");
            }

            try
            {
                var result = await _courseService.AddCourse(courseRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding course");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<CourseResponseDTO>>> SearchCourse(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return BadRequest("Search text is required.");
            }

            try
            {
                var result = await _courseService.SearchCourse(searchText);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error searching courses");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CourseResponseDTO>>> GetAllCourses()
        {
            try
            {
                var result = await _courseService.GetAllCourse();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all courses");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{courseId}")]
        public async Task<ActionResult<CourseResponseDTO>> GetCourseById(Guid courseId)
        {
            try
            {
                var result = await _courseService.GetCourseById(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting course by id {courseId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CourseResponseDTO>> UpdateCourse(Guid id, UpdateCourseRequestDTO courseRequest)
        {
            if (courseRequest == null)
            {
                return BadRequest("Update data is required.");
            }

            try
            {
                var result = await _courseService.UpdateCourse(id, courseRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating course with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{courseId}")]
        public async Task<ActionResult<string>> DeleteCourse(Guid courseId)
        {
            try
            {
                var result = await _courseService.DeleteCourse(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting course with id {courseId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<CoursePaginateResponseDTO>>> GetPaginatedCourses(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _courseService.GetPaginatedCourses(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated courses");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("image/{courseId}")]
        public async Task<ActionResult<string>> UploadImage(Guid courseId, IFormFile? image)
        {
            try
            {
                var data = await _courseService.UploadImage(courseId, image);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error uploading image for course id {courseId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Top3")]
        public async Task<ActionResult<IEnumerable<Top3CourseDTO>>> GetTop3Courses()
        {
            try
            {
                var data = await _courseService.GetTop3Courses();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting top 3 courses");
                return BadRequest(ex.Message);
            }
        }
    }
}
