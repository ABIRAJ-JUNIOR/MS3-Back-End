﻿using Microsoft.CodeAnalysis;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Service
{
    public class CourseSheduleService
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

            var CourseResponseList = new List<CourseSheduleResponseDTO>();
            foreach (var item in data)
            {
                var CourseResponse = new CourseSheduleResponseDTO
                {
                    Id = item.Id,
                    CourseId = item.CourseId,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Time = item.Time,
                    Location = item.Location,
                    MaxStudents = item.MaxStudents,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                    ScheduleStatus = item.ScheduleStatus,


                };

                CourseResponseList.Add(CourseResponse);

            }
            return CourseResponseList;

        }


    }
}
