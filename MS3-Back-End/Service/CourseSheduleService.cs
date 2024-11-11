using Microsoft.CodeAnalysis;
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



    }
}
