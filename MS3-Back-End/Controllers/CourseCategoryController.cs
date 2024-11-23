using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService _courseCategoryService;

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CourseCategoryRequestDTO courseCategoryRequestDTO)
        {
            try
            {
                var addCategory = await _courseCategoryService.AddCategory(courseCategoryRequestDTO);
                return Ok(addCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseCategoryById(Guid Id)
        {
            try
            {
                var result = await _courseCategoryService.GetCourseCategoryById(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourseCategory(CategoryUpdateRequestDTO courseCategoryRequestDTO)
        {
            try
            {
                var result = await _courseCategoryService.UpdateCourseCategory(courseCategoryRequestDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]

        public async Task<IActionResult> GetAllGetCourseCategory()
        {
            var result=await _courseCategoryService.GetAllGetCourseCategory();
            return Ok(result);
        }

    }
}
