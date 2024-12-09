using Azure;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.DTOs.ResponseDTOs.Student;

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
                CourseCategoryId = courseReq.CourseCategoryId,
                CourseName = courseReq.CourseName,
                Level = courseReq.Level,
                CourseFee = courseReq.CourseFee,
                Description = courseReq.Description,
                Prerequisites = courseReq.Prerequisites,
                ImageUrl = null,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            };

            var data = await _courseRepository.AddCourse(Course);

            var CourseResponse = new CourseResponseDTO
            {
                Id = data.Id,
                CourseCategoryId = data.CourseCategoryId,
                CourseName = data.CourseName,
                Level = ((CourseLevel)data.Level).ToString(),
                CourseFee = data.CourseFee,
                Description = data.Description,
                Prerequisites = data.Prerequisites,
                ImageUrl = data.ImageUrl!,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
            };

            return CourseResponse;

        }

        public async Task<ICollection<CourseResponseDTO>> SearchCourse(string SearchText)
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
                Level = ((CourseLevel)item.Level).ToString(),
                CourseFee = item.CourseFee,
                Description = item.Description,
                Prerequisites = item.Prerequisites,
                ImageUrl = item.ImageUrl!,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                Schedules = item.CourseSchedules != null ? item.CourseSchedules.Select(cs => new CourseScheduleResponseDTO()
                {
                    Id = cs.Id,
                    CourseId = cs.CourseId,
                    StartDate = cs.StartDate,
                    EndDate = cs.EndDate,
                    Duration = cs.Duration,
                    Time = cs.Time,
                    Location = cs.Location,
                    MaxStudents = cs.MaxStudents,
                    EnrollCount = cs.EnrollCount,
                    CreatedDate = cs.CreatedDate,
                    UpdatedDate = cs.UpdatedDate,
                    ScheduleStatus = ((ScheduleStatus)cs.ScheduleStatus).ToString()
                }).ToList() : null
            }).ToList();

            return CourseResponse;
        }

        public async Task<ICollection<CourseResponseDTO>> GetAllCourse()
        {
            var data = await _courseRepository.GetAllCourse();
            if (data == null)
            {
                throw new Exception("Courses Not Available");
            }

            var CourseResponse = data.Select(course => new CourseResponseDTO
            {
                Id = course.Id,
                CourseCategoryId = course.CourseCategoryId,
                CourseName = course.CourseName,
                Level = ((CourseLevel)course.Level).ToString(),
                CourseFee = course.CourseFee,
                Description = course.Description,
                Prerequisites = course.Prerequisites,
                ImageUrl = course.ImageUrl!,
                CreatedDate = course.CreatedDate,
                UpdatedDate = course.UpdatedDate,
                Schedules = course.CourseSchedules != null ? course.CourseSchedules.Select(cs => new CourseScheduleResponseDTO()
                {
                    Id = cs.Id,
                    CourseId = cs.CourseId,
                    StartDate = cs.StartDate,
                    EndDate = cs.EndDate,
                    Duration = cs.Duration,
                    Time = cs.Time,
                    Location = cs.Location,
                    MaxStudents = cs.MaxStudents,
                    EnrollCount = cs.EnrollCount,
                    CreatedDate = cs.CreatedDate,
                    UpdatedDate = cs.UpdatedDate,
                    ScheduleStatus = ((ScheduleStatus)cs.ScheduleStatus).ToString()
                }).ToList() : null,
                Feedbacks = course.Feedbacks?.Select(fb => new FeedbacksResponceDTO
                {
                    Id = fb.Id,
                    FeedBackText = fb.FeedBackText,
                    Rating = fb.Rating,
                    FeedBackDate = fb.FeedBackDate,
                    StudentId = fb.StudentId,
                    CourseId = fb.CourseId
                }).ToList() ?? new List<FeedbacksResponceDTO>()
            }).ToList();

            return CourseResponse;
        }


        public async Task<CourseResponseDTO> GetCourseById(Guid CourseId)
        {
            var data = await _courseRepository.GetCourseById(CourseId);

            if (data == null)
            {
                throw new Exception("course not found");  
            }

            var CourseResponse = new CourseResponseDTO
            {
                Id = data.Id,
                CourseCategoryId = data.CourseCategoryId,
                CourseName = data.CourseName,
                Level = ((CourseLevel)data.Level).ToString(),
                CourseFee = data.CourseFee,
                Description = data.Description,
                Prerequisites = data.Prerequisites,
                ImageUrl = data.ImageUrl!,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                Schedules = data.CourseSchedules?.Select(cs => new CourseScheduleResponseDTO()
                {
                    Id = cs.Id,
                    CourseId = cs.CourseId,
                    StartDate = cs.StartDate,
                    EndDate = cs.EndDate,
                    Duration = cs.Duration,
                    Time = cs.Time,
                    Location = cs.Location,
                    MaxStudents = cs.MaxStudents,
                    EnrollCount = cs.EnrollCount,
                    CreatedDate = cs.CreatedDate,
                    UpdatedDate = cs.UpdatedDate,
                    ScheduleStatus = ((ScheduleStatus)cs.ScheduleStatus).ToString()
                }).ToList(), 
                Feedbacks = data.Feedbacks?.Select(fb => new FeedbacksResponceDTO()
                {
                    Id = fb.Id,
                    FeedBackText = fb.FeedBackText,
                    Rating = fb.Rating,
                    FeedBackDate = fb.FeedBackDate,
                    StudentId = fb.StudentId,
                    CourseId = fb.CourseId,
                    Student = new StudentResponseDTO
                    {
                        Id = fb.Student.Id,
                        FirstName = fb.Student.FirstName,
                        LastName = fb.Student.LastName,
                        Phone = fb.Student.Phone,
                        ImageUrl = fb.Student.ImageUrl
                    }
                }).ToList()
            };

            return CourseResponse;
        }


        public async Task<CourseResponseDTO> UpdateCourse(Guid id, UpdateCourseRequestDTO course)
        {
          

            var GetData =await _courseRepository.GetCourseById(id);

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

            if (course.ImageUrl != null)
                GetData.ImageUrl = course.ImageUrl;


            GetData.UpdatedDate=DateTime.Now;

            var data =await _courseRepository.UpdateCourse(GetData);

            var CourseReturn = new CourseResponseDTO
            {
                Id = data.Id,
                CourseCategoryId = data.CourseCategoryId,
                CourseName = data.CourseName,
                Level = ((CourseLevel)data.Level).ToString(),
                CourseFee = data.CourseFee,
                Description = data.Description,
                Prerequisites = data.Prerequisites,
                ImageUrl = data.ImageUrl!,
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


        public async Task<PaginationResponseDTO<CourseResponseDTO>> GetPaginatedCourses(int pageNumber, int pageSize)
        {
            var courses = await _courseRepository.GetPaginatedCourses(pageNumber, pageSize);
            var allCourses = await _courseRepository.GetAllCourse();

            var courseResponses = courses.Select(course => new CourseResponseDTO
            {
                Id = course.Id,
                CourseCategoryId = course.CourseCategoryId,
                CourseName = course.CourseName,
                Level = ((CourseLevel)course.Level).ToString(),
                CourseFee = course.CourseFee,
                Description = course.Description,
                Prerequisites = course.Prerequisites,
                ImageUrl = course.ImageUrl!,
                CreatedDate = course.CreatedDate,
                UpdatedDate = course.UpdatedDate,

                Schedules = course.CourseSchedules?.Select(cs => new CourseScheduleResponseDTO
                {
                    Id = cs.Id,
                    CourseId = cs.CourseId,
                    StartDate = cs.StartDate,
                    EndDate = cs.EndDate,
                    Duration = cs.Duration,
                    Time = cs.Time,
                    Location = cs.Location,
                    MaxStudents = cs.MaxStudents,
                    EnrollCount = cs.EnrollCount,
                    CreatedDate = cs.CreatedDate,
                    UpdatedDate = cs.UpdatedDate,
                    ScheduleStatus = ((ScheduleStatus)cs.ScheduleStatus).ToString()
                }).ToList() ?? new List<CourseScheduleResponseDTO>(),  

                Feedbacks = course.Feedbacks?.Select(fb => new FeedbacksResponceDTO
                {
                    Id = fb.Id,
                    FeedBackText = fb.FeedBackText,
                    Rating = fb.Rating,
                    FeedBackDate = fb.FeedBackDate,
                    StudentId = fb.StudentId,
                    CourseId = fb.CourseId
                }).ToList() ?? new List<FeedbacksResponceDTO>()
            }).ToList();

            var paginationResponseDto = new PaginationResponseDTO<CourseResponseDTO>
            {
                Items = courseResponses,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allCourses.Count / (double)pageSize),
                TotalItem = allCourses.Count,
            };

            return paginationResponseDto;
        }
        public async Task<string> UploadImage(Guid CourseId, IFormFile? image)
        {
            var courseData = await _courseRepository.GetCourseById(CourseId);
            if (courseData == null)
            {
                throw new Exception("Course not found");
            }

            if(image == null)
            {
                throw new Exception($"Could not upload image");
            }

            var cloudinaryUrl = "cloudinary://779552958281786:JupUDaXM2QyLcruGYFayOI1U9JI@dgpyq5til";

            Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);

            using (var stream = image.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                courseData.ImageUrl = (uploadResult.SecureUrl).ToString();
            }
            var updatedData = await _courseRepository.UpdateCourse(courseData);

            return "Image upload successfully";
        }

        public async Task<ICollection<Top3CourseDTO>> GetTop3Courses()
        {
            return await _courseRepository.GetTop3Courses();
        }
    }
}
