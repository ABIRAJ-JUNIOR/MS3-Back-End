using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment> AddEnrollment(Enrollment Enrollment);
        Task<ICollection<Enrollment>> SearchEnrollments(Guid SearchId);
        Task<ICollection<Enrollment>> GetEnrollments();
        Task<Enrollment> GetEnrollmentById(Guid EnrollmentId);
        Task<string> DeleteEnrollment(Enrollment course);
    }
}