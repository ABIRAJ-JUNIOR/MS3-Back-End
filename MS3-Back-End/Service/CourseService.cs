using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public async Task<CourseResponseDTO> AddCourse(CourseRequestDTO courseReq)
        {

            var Course = new Course
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

            var data = await _courseRepository.AddCourse(Course);

            var CourseResponse = new CourseResponseDTO
            {
                Id = data.Id,
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

            return CourseResponse;

        }

        public async Task<List<CourseResponseDTO>> SearchCourse(string SearchText)
        {
            var data = await _courseRepository.SearchCourse(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var CourseResponse = new List<CourseResponseDTO>();
            foreach (var item in data)
            {
                var obj = new CourseResponseDTO
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    CourseName = item.CourseName,
                    Level = item.Level,
                    CourseFee = item.CourseFee,
                    Description = item.Description,
                    Prerequisites = item.Prerequisites,
                    ImagePath = item.ImagePath,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate
                };
                CourseResponse.Add(obj);

            }
            return CourseResponse;

        }

        public async Task<List<CourseResponseDTO>> GetAllCourse()
        {
            var data = await _courseRepository.GetAllCourse();
            if (data == null)
            {
                throw new Exception("Courses Not Available");
            }
            var CourseResponse= new List<CourseResponseDTO>();
            foreach (var item in data)
            {
                var obj = new CourseResponseDTO
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    CourseName = item.CourseName,
                    Level = item.Level,
                    CourseFee = item.CourseFee,
                    Description = item.Description,
                    Prerequisites = item.Prerequisites,
                    ImagePath = item.ImagePath,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate

                };
                CourseResponse.Add(obj);
            }
            return CourseResponse;
        }


        public async Task<CourseResponseDTO> GetCourseById(Guid CourseId)
        {
            var data = await _courseRepository.GetCourseById(CourseId);
            if (data == null)
            {
                throw new Exception("Course Not Found");
            }
            var CourseResponse = new CourseResponseDTO
            {
                Id = data.Id,
                CategoryId = data.CategoryId,
                CourseName = data.CourseName,
                CourseFee = data.CourseFee,
                Description = data.Description,
                Prerequisites = data.Prerequisites,
                ImagePath = data.ImagePath,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate
            };
            return CourseResponse;
        }

       
        public async Task<CourseResponseDTO> UpdateCourse(UpdateCourseRequestDTO course)
        {
          

            var GetData =await _courseRepository.GetCourseById(course.Id);

            if (course.CategoryId.HasValue)
                GetData.CategoryId = course.CategoryId.Value;

            if (!string.IsNullOrEmpty(course.CourseName))
                GetData.CourseName = course.CourseName;

            if (course.Level.HasValue)
                GetData.Level = course.Level.Value;

            if (course.CourseFee.HasValue)
                GetData.CourseFee = course.CourseFee.Value;

            if (!string.IsNullOrEmpty(course.Description))
                GetData.Description = course.Description;

            if (!string.IsNullOrEmpty(course.Prerequisites))
                GetData.Prerequisites = course.Prerequisites;

            if (!string.IsNullOrEmpty(course.ImagePath))
                GetData.ImagePath = course.ImagePath;


            GetData.UpdatedDate=DateTime.Now;

            var data =await _courseRepository.UpdateCourse(GetData);

            var CourseReturn = new CourseResponseDTO
            {
                Id = data.Id,
                CategoryId = data.CategoryId,
                CourseName = data.CourseName,
                Level = data.Level,
                CourseFee = data.CourseFee,
                Description = data.Description,
                Prerequisites = data.Prerequisites,
                ImagePath = data.ImagePath,
                UpdatedDate = data.UpdatedDate,
                CreatedDate = data.CreatedDate,

            };
            return CourseReturn;

        }


        public async Task<string> DeleteCourse(Guid Id)
        {
            var GetData = await _courseRepository.GetCourseById(Id);
            GetData.IsDeleted = true;
            if(GetData == null)
            {
                throw new Exception("Course Id not Found");
            }
            var data = await _courseRepository.DeleteCourse(GetData);
            return data;
        }

    }



}
