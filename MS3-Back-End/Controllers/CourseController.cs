using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;

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

        [HttpPost("course")]
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

        [HttpGet("search")]
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

        [HttpGet("course")]
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

        [HttpGet("courseById")]
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


        [HttpPut("course")]
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


        [HttpDelete("course/{CourseId}")]
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

    }
}
