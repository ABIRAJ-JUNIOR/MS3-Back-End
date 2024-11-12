using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.CourseCategory;
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

        HttpPost("Add-Category")]

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

    }
}
