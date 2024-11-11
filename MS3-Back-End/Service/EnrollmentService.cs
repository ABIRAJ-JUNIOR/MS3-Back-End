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


        public async Task<List<EnrollmentResponseDTO>> SearchCourseByUserId(Guid SearchUserId)
        {
            var data = await _enrollmentRepository.SearchEnrollments(SearchUserId);
            if (data == null)
            {
                throw new Exception("Search Not Found");
            }

            var ListEnrollment = new List<EnrollmentResponseDTO>();
            foreach (var item in data)
            {
                var EnrollmentResponse = new EnrollmentResponseDTO
                {
                    Id = item.Id,
                    StudentId = item.StudentId,
                    CourseSheduleId = item.CourseSheduleId,
                    EnrollmentDate = item.EnrollmentDate,
                    PaymentStatus = item.PaymentStatus,
                    IsActive = item.IsActive
                };
                ListEnrollment.Add(EnrollmentResponse);

            }

            return ListEnrollment;

        }


        public async Task<List<EnrollmentResponseDTO>> GetAllEnrollements()
        {
            var data = await _enrollmentRepository.GetEnrollments();
            if (data == null)
            {
                throw new Exception("Enrollment Not Available");
            }
            var ListEnrollment = new List<EnrollmentResponseDTO>();
            foreach (var item in data)
            {
                var EnrollmentResponse = new EnrollmentResponseDTO
                {
                    Id = item.Id,
                    StudentId = item.StudentId,
                    CourseSheduleId = item.CourseSheduleId,
                    EnrollmentDate = item.EnrollmentDate,
                    PaymentStatus = item.PaymentStatus,
                    IsActive = item.IsActive
                };
                ListEnrollment.Add(EnrollmentResponse);

            }

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



        public async Task<EnrollmentResponseDTO> UpdateEnrollment(EnrollmentUpdateDTO enrollment)
        {
            var getData = await _enrollmentRepository.GetEnrollmentById(enrollment.Id);

            if (enrollment.EnrollmentDate.HasValue)
            {
                getData.EnrollmentDate = enrollment.EnrollmentDate.Value;
            }

            if (enrollment.PaymentStatus.HasValue)
            {
                getData.PaymentStatus = enrollment.PaymentStatus.Value;
            }

            if (enrollment.IsActive.HasValue)
                getData.IsActive = enrollment.IsActive.Value;

            if (enrollment.StudentId.HasValue)
            {
                getData.StudentId = enrollment.StudentId.Value;
            }

            if (enrollment.CourseSheduleId.HasValue)
            {
                getData.CourseSheduleId = enrollment.CourseSheduleId.Value;
            }


            var updatedData = await _enrollmentRepository.UpdateEnrollment(getData);

            var enrollmentResponse = new EnrollmentResponseDTO
            {
                Id = updatedData.Id,
                EnrollmentDate = updatedData.EnrollmentDate,
                PaymentStatus = updatedData.PaymentStatus,
                IsActive = updatedData.IsActive,
                StudentId = updatedData.StudentId,
                CourseSheduleId = updatedData.CourseSheduleId,

            };

            return enrollmentResponse;
        }


        public async Task<string> DeleteEnrollment(Guid Id)
        {
            var GetData = await _enrollmentRepository.GetEnrollmentById(Id);
            GetData.IsActive = false;
            if (GetData == null)
            {
                throw new Exception("Course Id not Found");
            }
            var data = await _enrollmentRepository.DeleteEnrollment(GetData);
            return data;
        }








    }
}
