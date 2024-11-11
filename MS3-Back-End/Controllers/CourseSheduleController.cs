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

    }
}
