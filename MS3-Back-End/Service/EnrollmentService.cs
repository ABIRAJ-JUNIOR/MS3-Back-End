using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq)
        {

            var Enrollment = new Enrollment
            {
                StudentId = EnrollmentReq.StudentId,
                CourseSheduleId = EnrollmentReq.CourseSheduleId,
                EnrollmentDate = EnrollmentReq.EnrollmentDate,
                PaymentStatus = EnrollmentReq.PaymentStatus,

            };

            var data = await _enrollmentRepository.AddEnrollment(Enrollment);

            var EnrollmentResponse = new EnrollmentResponseDTO
            {
                Id=data.Id,
                StudentId = data.StudentId,
                CourseSheduleId = data.CourseSheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = data.PaymentStatus,
                IsActive = data.IsActive
            };

            return EnrollmentResponse;

        }





    }
}
