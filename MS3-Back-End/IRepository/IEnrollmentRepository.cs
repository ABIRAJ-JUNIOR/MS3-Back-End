using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> AddEnrollment(Enrollment Enrollment);
        Task<ICollection<Enrollment>> GetEnrollmentsByStudentId(Guid studentId);
        Task<ICollection<Enrollment>> GetEnrollments();
        Task<Enrollment> GetEnrollmentById(Guid EnrollmentId);
        Task<Enrollment> UpdateEnrollment(Enrollment Enrollment);
        Task<string> DeleteEnrollment(Enrollment course);
    }
}