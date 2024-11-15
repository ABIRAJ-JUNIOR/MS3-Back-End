using Microsoft.CodeAnalysis;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Service
{
    public class CourseSheduleService : ICourseSheduleService
    {
        private readonly ICourseSheduleRepository _courseSheduleRepository;
        public CourseSheduleService(ICourseSheduleRepository courseSheduleRepository)
        {
            _courseSheduleRepository = courseSheduleRepository;
        }


        public async Task<CourseSheduleResponseDTO> AddCourseShedule(CourseSheduleRequestDTO courseReq)
        {
            var CourseShedule = new CourseSchedule
            {
                CourseId = courseReq.CourseId,
                StartDate = courseReq.StartDate,
                EndDate = courseReq.EndDate,
                Duration = (courseReq.EndDate - courseReq.StartDate).Days,
                Time = courseReq.Time,
                Location = courseReq.Location,
                MaxStudents = courseReq.MaxStudents,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ScheduleStatus = courseReq.ScheduleStatus,

            };

            var data = await _courseSheduleRepository.AddCourseShedule(CourseShedule);

            var CourseResponse = new CourseSheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = data.ScheduleStatus,

            };

            return CourseResponse;

        }

        public async Task<List<CourseSheduleResponseDTO>> SearchCourseShedule(string SearchText)
        {
            var data = await _courseSheduleRepository.SearchSheduleLocation(SearchText);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var CourseResponseList = data.Select(item => new CourseSheduleResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Duration = item.Duration,
                Time = item.Time,
                Location = item.Location,
                MaxStudents = item.MaxStudents,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                ScheduleStatus = item.ScheduleStatus,
            }).ToList();
            
            return CourseResponseList;

        }

        public async Task<List<CourseSheduleResponseDTO>> GetAllCourseShedule()
        {
            var data = await _courseSheduleRepository.GetAllCourseShedule();
            if (data == null)
            {
                throw new Exception("Courses Not Available");
            }
            var CourseResponseList = data.Select(item => new CourseSheduleResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Duration = item.Duration,
                Time = item.Time,
                Location = item.Location,
                MaxStudents = item.MaxStudents,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                ScheduleStatus = item.ScheduleStatus,
            }).ToList();

            return CourseResponseList;
        }


        public async Task<CourseSheduleResponseDTO> GetCourseSheduleById(Guid id)
        {
            var data = await _courseSheduleRepository.GetCourseSheduleById(id);
            if (data == null)
            {
                throw new Exception("Course Not Found");
            }
            var CourseResponse = new CourseSheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = data.ScheduleStatus,
            };

            return CourseResponse;
        }


        public async Task<CourseSheduleResponseDTO> UpdateCourseShedule(UpdateCourseSheduleDTO courseReq)
        {

            var getData = await _courseSheduleRepository.GetCourseSheduleById(courseReq.Id);
            if(getData == null)
            {
                throw new Exception("Course Shedule not found");
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


            var data = await _courseSheduleRepository.UpdateCourseShedule(getData);

            var CourseResponse = new CourseSheduleResponseDTO
            {
                Id = data.Id,
                CourseId = data.CourseId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Duration = data.Duration,
                Time = data.Time,
                Location = data.Location,
                MaxStudents = data.MaxStudents,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                ScheduleStatus = data.ScheduleStatus,

            };

            return CourseResponse;

        }
    }
}
