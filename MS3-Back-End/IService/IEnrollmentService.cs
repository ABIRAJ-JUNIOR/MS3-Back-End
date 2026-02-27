using MS3_Back_End.DTOs.RequestDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;

namespace MS3_Back_End.IService
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq);
        Task<ICollection<EnrollmentResponseDTO>> GetEnrollmentsByStudentId(Guid studentId);
        Task<ICollection<EnrollmentResponseDTO>> GetAllEnrollements();
        Task<EnrollmentResponseDTO> GetEnrollmentId(Guid EnrollmentId);
        Task<string> DeleteEnrollment(Guid Id);
    }
}