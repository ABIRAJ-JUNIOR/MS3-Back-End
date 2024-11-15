using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseService(ICourseRepository courseRepository, IWebHostEnvironment webHostEnvironment)
        {
            _courseRepository = courseRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CourseResponseDTO> AddCourse([FromForm]CourseRequestDTO courseReq)
        {

            var Course = new Course
            {
                CourseCategoryId = courseReq.CourseCategoryId,
                CourseName = courseReq.CourseName,
                Level = courseReq.Level,
                CourseFee = courseReq.CourseFee,
                Description = courseReq.Description,
                Prerequisites = courseReq.Prerequisites,
                ImagePath = await SaveImageFile(courseReq.ImageFile!),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var data = await _courseRepository.AddCourse(Course);

            var CourseResponse = new CourseResponseDTO
            {
                Id = data.Id,
                CourseCategoryId = data.CourseCategoryId,
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

            var CourseResponse = data.Select(item => new CourseResponseDTO()
            {
                Id = item.Id,
                CourseCategoryId = item.CourseCategoryId,
                CourseName = item.CourseName,
                Level = item.Level,
                CourseFee = item.CourseFee,
                Description = item.Description,
                Prerequisites = item.Prerequisites,
                ImagePath = item.ImagePath,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate
            }).ToList();

            return CourseResponse;
        }

        public async Task<List<CourseResponseDTO>> GetAllCourse()
        {
            var data = await _courseRepository.GetAllCourse();
            if (data == null)
            {
                throw new Exception("Courses Not Available");
            }

            var CourseResponse = data.Select(item => new CourseResponseDTO()
            {
                Id = item.Id,
                CourseCategoryId = item.CourseCategoryId,
                CourseName = item.CourseName,
                Level = item.Level,
                CourseFee = item.CourseFee,
                Description = item.Description,
                Prerequisites = item.Prerequisites,
                ImagePath = item.ImagePath,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate
            }).ToList();

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
                CourseCategoryId = data.CourseCategoryId,
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
                GetData.CourseCategoryId = course.CategoryId.Value;

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

            if (course.ImageFile != null)
                GetData.ImagePath = await SaveImageFile(course.ImageFile!);


            GetData.UpdatedDate=DateTime.Now;

            var data =await _courseRepository.UpdateCourse(GetData);

            var CourseReturn = new CourseResponseDTO
            {
                Id = data.Id,
                CourseCategoryId = data.CourseCategoryId,
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
            if(GetData == null)
            {
                throw new Exception("Course not Found");
            }

            GetData.IsDeleted = true;

            var data = await _courseRepository.DeleteCourse(GetData);
            return data;
        }

        private async Task<string> SaveImageFile(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Course");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/Course/{fileName}";
        }
    }
}
