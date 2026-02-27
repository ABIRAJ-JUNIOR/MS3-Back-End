using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;
using MS3_Back_End.IService;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpPost]
        public async Task<ActionResult<CourseCategoryResponseDTO>> AddCategory(CourseCategoryRequestDTO courseCategoryRequestDTO)
        {
            if (courseCategoryRequestDTO == null)
            {
                return BadRequest("Course category data is required.");
            }

            try
            {
                var addCategory = await _courseCategoryService.AddCategory(courseCategoryRequestDTO);
                return Ok(addCategory);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding course category");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseCategoryResponseDTO>> GetCourseCategoryById(Guid id)
        {
            try
            {
                var result = await _courseCategoryService.GetCourseCategoryById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting course category by id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CourseCategoryResponseDTO>> UpdateCourseCategory(CategoryUpdateRequestDTO courseCategoryRequestDTO)
        {
            if (courseCategoryRequestDTO == null)
            {
                return BadRequest("Update data is required.");
            }

            try
            {
                var result = await _courseCategoryService.UpdateCourseCategory(courseCategoryRequestDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating course category");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<IEnumerable<CourseCategoryResponseDTO>>> GetAllCourseCategories()
        {
            try
            {
                var result = await _courseCategoryService.GetAllGetCourseCategory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all course categories");
                return BadRequest(ex.Message);
            }
        }
    }
}
