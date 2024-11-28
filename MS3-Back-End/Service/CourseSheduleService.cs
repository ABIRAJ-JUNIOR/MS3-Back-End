using Microsoft.CodeAnalysis;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class CourseScheduleService : ICourseScheduleService
    {
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        public CourseScheduleService(ICourseScheduleRepository courseScheduleRepository)
        {
            _courseScheduleRepository = courseScheduleRepository;
        }


        public async Task<CourseScheduleResponseDTO> AddCourseSchedule(CourseScheduleRequestDTO courseReq)
        {
            if (courseReq.StartDate < DateTime.UtcNow)
            {
                throw new ArgumentException("The course start date cannot be in the past. Please select a valid future date.");
            }

            if (courseReq.EndDate < courseReq.StartDate)
            {
                throw new ArgumentException("The course end date cannot be earlier than the start date. Please select a valid end date.");
            }

            if (courseReq.MaxStudents <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(courseReq.MaxStudents), "The maximum number of students must be greater than 0.");
            }


            var CourseSchedule = new CourseSchedule
            {
                CourseId = courseReq.CourseId,
                StartDate = courseReq.StartDate,
                EndDate = courseReq.EndDate,
                Duration = (courseReq.EndDate - courseReq.StartDate).Days,
                Time = courseReq.Time,
                Location = courseReq.Location,
                MaxStudents = courseReq.MaxStudents,
                EnrollCount = 0,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ScheduleStatus = courseReq.ScheduleStatus,

            };

            var data = await _courseScheduleRepository.AddCourseSchedule(CourseSchedule);

            var CourseResponse = new CourseScheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                EnrollCount = data.EnrollCount,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = ((ScheduleStatus)data.ScheduleStatus).ToString(),

            };

            return CourseResponse;

        }

        public async Task<ICollection<CourseScheduleResponseDTO>> SearchCourseSchedule(string SearchText)
        {
            var data = await _courseScheduleRepository.SearchScheduleLocation(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var CourseResponseList = data.Select(item => new CourseScheduleResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Duration = item.Duration,
                Time = item.Time,
                Location = item.Location,
                MaxStudents = item.MaxStudents,
                EnrollCount = item.EnrollCount,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                ScheduleStatus = ((ScheduleStatus)item.ScheduleStatus).ToString(),
            }).ToList();

            return CourseResponseList;

        }

        public async Task<ICollection<CourseScheduleResponseDTO>> GetAllCourseSchedule()
        {
            var data = await _courseScheduleRepository.GetAllCourseSchedule();
            if (data == null)
            {
                throw new Exception("Courses Not Available");
            }
            var CourseResponseList = data.Select(item => new CourseScheduleResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Duration = item.Duration,
                Time = item.Time,
                Location = item.Location,
                MaxStudents = item.MaxStudents,
                EnrollCount = item.EnrollCount,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                ScheduleStatus = ((ScheduleStatus)item.ScheduleStatus).ToString(),
            }).ToList();

            return CourseResponseList;
        }


        public async Task<CourseScheduleResponseDTO> GetCourseScheduleById(Guid id)
        {
            var data = await _courseScheduleRepository.GetCourseScheduleById(id);
            if (data == null)
            {
                throw new Exception("Course Not Found");
            }
            var CourseResponse = new CourseScheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                EnrollCount = data.EnrollCount,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = ((ScheduleStatus)data.ScheduleStatus).ToString(),
            };

            return CourseResponse;
        }


        public async Task<CourseScheduleResponseDTO> UpdateCourseSchedule(Guid id ,UpdateCourseScheduleDTO courseReq)
        {

            var getData = await _courseScheduleRepository.GetCourseScheduleById(id);
            if (getData == null)
            {
                throw new Exception("Course Schedule not found");
            }

            getData.CourseId = courseReq.CourseId;
            getData.StartDate = courseReq.StartDate;
            getData.EndDate = courseReq.EndDate;
            getData.Duration = (courseReq.EndDate - courseReq.StartDate).Days;
            getData.Time = courseReq.Time;
            getData.Location = courseReq.Location;
            getData.MaxStudents = courseReq.MaxStudents;
            getData.ScheduleStatus = courseReq.ScheduleStatus;
            getData.UpdatedDate = DateTime.Now;


            var data = await _courseScheduleRepository.UpdateCourseSchedule(getData);

            var CourseResponse = new CourseScheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                EnrollCount = data.EnrollCount,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = ((ScheduleStatus)data.ScheduleStatus).ToString(),

            };

            return CourseResponse;

        }

        public async Task<PaginationResponseDTO<CourseScheduleResponseDTO>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize)
        {
            var allSchedules = await _courseScheduleRepository.GetAllCourseSchedule();
            var paginatedSchedules = await _courseScheduleRepository.GetPaginatedCoursesSchedules(pageNumber, pageSize);

            var courseScheduleResponse = paginatedSchedules.Select(cs => new CourseScheduleResponseDTO
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
                ScheduleStatus = ((ScheduleStatus)cs.ScheduleStatus).ToString(),
                CourseResponse =cs.Course != null ?  new CourseResponseDTO()
                {
                    Id = cs.Course.Id,
                    CourseCategoryId = cs.Course.CourseCategoryId,
                    CourseName = cs.Course.CourseName,
                    Level = ((CourseLevel)cs.Course.Level).ToString(),
                    CourseFee = cs.Course.CourseFee,
                    Description = cs.Course.Description,
                    Prerequisites = cs.Course.Prerequisites,
                    ImageUrl = cs.Course.ImageUrl!,
                    CreatedDate = cs.Course.CreatedDate,
                    UpdatedDate = cs.Course.UpdatedDate,
                } : null,
            }).ToList();

            var paginationResponseDto = new PaginationResponseDTO<CourseScheduleResponseDTO>
            {
                Items = courseScheduleResponse,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allSchedules.Count / (double)pageSize),
                TotalItem = allSchedules.Count,
            };

            return paginationResponseDto;
        }
    }
}
