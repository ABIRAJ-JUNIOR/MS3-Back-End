using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.ResponseDTOs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Service
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
             _courseRepository = courseRepository;
        }


        async Task<CourseResponseDTO> AddCourse(CourseRequestDTO courseReq)
        {
            
                var obj = new Course
                {
                    CategoryId = courseReq.CategoryId,
                    CourseName = courseReq.CourseName,
                    Level = courseReq.Level,
                    CourseFee = courseReq.CourseFee,
                    Description = courseReq.Description,
                    Prerequisites = courseReq.Prerequisites,
                    ImagePath = courseReq.ImagePath,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var data = await _courseRepository.AddCourse(obj);

                var ReturnObj = new CourseResponseDTO
                {
                    CategoryId = data.CategoryId,
                    CourseName = data.CourseName,
                    Level = data.Level,
                    CourseFee = data.CourseFee,
                    Description = data.Description,
                    Prerequisites = data.Prerequisites,
                    ImagePath = data.ImagePath,
                    CreatedDate = data.CreatedDate,
                    UpdatedDate = data.UpdatedDate
                };
           
        }





    }
}
