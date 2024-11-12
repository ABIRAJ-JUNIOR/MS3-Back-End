using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> AddEnrollment(Enrollment Enrollment);
        Task<List<Enrollment>> SearchEnrollments(Guid SearchId);
        Task<List<Enrollment>> GetEnrollments();
        Task<Enrollment> GetEnrollmentById(Guid EnrollmentId);
        Task<Enrollment> UpdateEnrollment(Enrollment course);
        Task<string> DeleteEnrollment(Enrollment course);
    }
}