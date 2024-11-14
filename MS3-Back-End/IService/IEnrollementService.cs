using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;

namespace MS3_Back_End.IService
{
    public interface IEnrollementService
    {
        Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq);
        Task<List<EnrollmentResponseDTO>> SearchEnrollmentByUserId(Guid SearchUserId);
        Task<List<EnrollmentResponseDTO>> GetAllEnrollements();
        Task<EnrollmentResponseDTO> GetEnrollmentId(Guid EnrollmentId);
        Task<EnrollmentResponseDTO> UpdateEnrollment(EnrollmentUpdateDTO enrollment);
        Task<string> DeleteEnrollment(Guid Id);
    }
}