using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
           _courseService = courseService;
        }

        [HttpPost("Course")]
        public async Task<IActionResult> AddCourse(CourseRequestDTO courseRequest)
        {
            try
            {
                var result = await _courseService.AddCourse(courseRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCourse(string searchText)
        {
            try
            {
                var result = await _courseService.SearchCourse(searchText);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var result = await _courseService.GetAllCourse();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("CourseById/{CourseId}")]
        public async Task<IActionResult> GetCourseById(Guid CourseId)
        {
            try
            {
                var result = await _courseService.GetCourseById(CourseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Course")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseRequestDTO courseRequest)
        {
            try
            {
                var result = await _courseService.UpdateCourse(courseRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


        [HttpDelete("Course/{CourseId}")]
        public async Task<IActionResult> DeleteCourse(Guid CourseId)
        {
            try
            {
                var result = await _courseService.DeleteCourse(CourseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedCourses(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _courseService.GetPaginatedCourses(pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(Guid CourseId, IFormFile image)
        {
            try
            {
                var data = await _courseService.UploadImage(CourseId,image);
                return Ok(data);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
