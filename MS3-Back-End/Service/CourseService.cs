﻿using Azure;
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
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<CourseResponseDTO> AddCourse(CourseRequestDTO courseReq)
        {
            if (courseReq == null)
            {
                throw new ArgumentNullException(nameof(courseReq));
            }

            var course = new Course
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

            var data = await _courseRepository.AddCourse(course);

            var courseResponse = new CourseResponseDTO
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

            _logger.LogInformation("Course added successfully with Id: {Id}", data.Id);

            return courseResponse;
        }

        public async Task<ICollection<CourseResponseDTO>> SearchCourse(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                throw new ArgumentException("Search text cannot be null or empty", nameof(searchText));
            }

            var data = await _courseRepository.SearchCourse(searchText);
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No courses found for search text: {SearchText}", searchText);
                throw new KeyNotFoundException("Search not found");
            }

            var courseResponse = data.Select(item => new CourseResponseDTO
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
                Schedules = item.CourseSchedules?.Select(cs => new CourseScheduleResponseDTO
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
                Feedbacks = item.Feedbacks?.Select(fb => new FeedbacksResponceDTO
                {
                    Id = fb.Id,
                    FeedBackText = fb.FeedBackText,
                    Rating = fb.Rating,
                    FeedBackDate = fb.FeedBackDate,
                    StudentId = fb.StudentId,
                    CourseId = fb.CourseId
                }).ToList()
            }).ToList();

            return courseResponse;
        }

        public async Task<ICollection<CourseResponseDTO>> GetAllCourse()
        {
            var data = await _courseRepository.GetAllCourse();
            if (data == null || !data.Any())
            {
                _logger.LogWarning("No courses available");
                throw new KeyNotFoundException("Courses not available");
            }

            var courseResponse = data.Select(course => new CourseResponseDTO
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
                }).ToList(),
                Feedbacks = course.Feedbacks?.Select(fb => new FeedbacksResponceDTO
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
            }).ToList();

            return courseResponse;
        }

        public async Task<CourseResponseDTO> GetCourseById(Guid courseId)
        {
            var data = await _courseRepository.GetCourseById(courseId);
            if (data == null)
            {
                _logger.LogWarning("Course not found for Id: {Id}", courseId);
                throw new KeyNotFoundException("Course not found");
            }

            var courseResponse = new CourseResponseDTO
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
                Schedules = data.CourseSchedules?.Select(cs => new CourseScheduleResponseDTO
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
                Feedbacks = data.Feedbacks?.Select(fb => new FeedbacksResponceDTO
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

            return courseResponse;
        }

        public async Task<CourseResponseDTO> UpdateCourse(Guid id, UpdateCourseRequestDTO course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            var getData = await _courseRepository.GetCourseById(id);
            if (getData == null)
            {
                _logger.LogWarning("Course not found for Id: {Id}", id);
                throw new KeyNotFoundException("Course not found");
            }

            if (course.CategoryId.HasValue)
                getData.CourseCategoryId = course.CategoryId.Value;

            if (!string.IsNullOrEmpty(course.CourseName))
                getData.CourseName = course.CourseName;

            if (course.Level.HasValue)
                getData.Level = course.Level.Value;

            if (course.CourseFee.HasValue)
                getData.CourseFee = course.CourseFee.Value;

            if (!string.IsNullOrEmpty(course.Description))
                getData.Description = course.Description;

            if (!string.IsNullOrEmpty(course.Prerequisites))
                getData.Prerequisites = course.Prerequisites;

            if (course.ImageUrl != null)
                getData.ImageUrl = course.ImageUrl;

            getData.UpdatedDate = DateTime.Now;

            var data = await _courseRepository.UpdateCourse(getData);

            var courseReturn = new CourseResponseDTO
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

            _logger.LogInformation("Course updated successfully with Id: {Id}", data.Id);

            return courseReturn;
        }

        public async Task<string> DeleteCourse(Guid id)
        {
            var getData = await _courseRepository.GetCourseById(id);
            if (getData == null)
            {
                _logger.LogWarning("Course not found for Id: {Id}", id);
                throw new KeyNotFoundException("Course not found");
            }

            getData.IsDeleted = true;

            var data = await _courseRepository.DeleteCourse(getData);

            _logger.LogInformation("Course deleted successfully with Id: {Id}", id);

            return data;
        }

        public async Task<PaginationResponseDTO<CoursePaginateResponseDTO>> GetPaginatedCourses(int pageNumber, int pageSize)
        {
            var courses = await _courseRepository.GetPaginatedCourses(pageNumber, pageSize);
            var allCourses = await _courseRepository.GetAllCourse();

            var courseResponses = courses.Select(course => new CoursePaginateResponseDTO
            {
                Id = course.Id,
                CourseCategoryId = course.CourseCategoryId,
                CourseName = course.CourseName,
                Level = ((CourseLevel)course.Level).ToString(),
                CourseFee = course.CourseFee,
                Description = course.Description,
                Prerequisites = course.Prerequisites,
                ImageUrl = course.ImageUrl!,
                FeedBackRate = course.Feedbacks!.Any() ? (int)Math.Round(course.Feedbacks!.Average(f => f.Rating), 1) : 0,
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

            var paginationResponseDto = new PaginationResponseDTO<CoursePaginateResponseDTO>
            {
                Items = courseResponses,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allCourses.Count / (double)pageSize),
                TotalItem = allCourses.Count,
            };

            return paginationResponseDto;
        }

        public async Task<string> UploadImage(Guid courseId, IFormFile? image)
        {
            var courseData = await _courseRepository.GetCourseById(courseId);
            if (courseData == null)
            {
                _logger.LogWarning("Course not found for Id: {Id}", courseId);
                throw new KeyNotFoundException("Course not found");
            }

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Image file cannot be null");
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
                courseData.ImageUrl = uploadResult.SecureUrl.ToString();
            }

            await _courseRepository.UpdateCourse(courseData);

            _logger.LogInformation("Image uploaded successfully for CourseId: {CourseId}", courseId);

            return "Image uploaded successfully";
        }

        public async Task<ICollection<Top3CourseDTO>> GetTop3Courses()
        {
            return await _courseRepository.GetTop3Courses();
        }
    }
}
