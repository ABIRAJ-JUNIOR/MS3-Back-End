using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class EnrollmentService :IEnrollementService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseSheduleRepository _courseSheduleRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseSheduleRepository courseSheduleRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseSheduleRepository = courseSheduleRepository;
        }

        public async Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq)
        {
            var courseSheduleData = await _courseSheduleRepository.GetCourseSheduleById(EnrollmentReq.CourseSheduleId);
            if(courseSheduleData == null)
            {
                throw new Exception("CourseShedule not found");
            }

            if(courseSheduleData.MaxStudents == 0)
            {
                throw new Exception("Reach limit");
            }

            courseSheduleData.MaxStudents = courseSheduleData.MaxStudents - 1;
            await _courseSheduleRepository.UpdateCourseShedule(courseSheduleData);

            var Enrollment = new Enrollment
            {
                StudentId = EnrollmentReq.StudentId,
                CourseSheduleId = EnrollmentReq.CourseSheduleId,
                EnrollmentDate = DateTime.Now,
                PaymentStatus = EnrollmentReq.PaymentStatus,
                IsActive = true,
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


        public async Task<List<EnrollmentResponseDTO>> SearchEnrollmentByUserId(Guid SearchUserId)
        {
            var data = await _enrollmentRepository.SearchEnrollments(SearchUserId);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var ListEnrollment = data.Select(item => new EnrollmentResponseDTO()
            {
                Id = item.Id,
                StudentId = item.StudentId,
                CourseSheduleId = item.CourseSheduleId,
                EnrollmentDate = item.EnrollmentDate,
                PaymentStatus = item.PaymentStatus,
                IsActive = item.IsActive
            }).ToList();    

            return ListEnrollment;
        }


        public async Task<List<EnrollmentResponseDTO>> GetAllEnrollements()
        {
            var data = await _enrollmentRepository.GetEnrollments();
            if (data == null)
            {
                throw new Exception("Enrollment Not Available");
            }
            var ListEnrollment = data.Select(item => new EnrollmentResponseDTO()
            {
                Id = item.Id,
                StudentId = item.StudentId,
                CourseSheduleId = item.CourseSheduleId,
                EnrollmentDate = item.EnrollmentDate,
                PaymentStatus = item.PaymentStatus,
                IsActive = item.IsActive
            }).ToList();

            return ListEnrollment;
        }

        public async Task<EnrollmentResponseDTO> GetEnrollmentId(Guid EnrollmentId)
        {
            var data = await _enrollmentRepository.GetEnrollmentById(EnrollmentId);
            if (data == null)
            {
                throw new Exception("Enrollment Not Found");
            }
            var EnrollmentResponse = new EnrollmentResponseDTO
            {
                Id = data.Id,
                StudentId = data.StudentId,
                CourseSheduleId = data.CourseSheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = data.PaymentStatus,
                IsActive = data.IsActive
            };

            return EnrollmentResponse;
        }

        public async Task<string> DeleteEnrollment(Guid Id)
        {
            var GetData = await _enrollmentRepository.GetEnrollmentById(Id);
            if (GetData == null)
            {
                throw new Exception("Course Id not Found");
            }
            GetData.IsActive = false;
            var data = await _enrollmentRepository.DeleteEnrollment(GetData);
            return data;
        }

    }
}
