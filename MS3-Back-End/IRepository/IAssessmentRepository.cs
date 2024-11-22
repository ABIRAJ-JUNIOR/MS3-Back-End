using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAssessmentRepository
    {
        Task<Assessment> AddAssessment(Assessment assessment);
        Task<ICollection<Assessment>> GetAllAssessment();
        Task<Assessment> UpdateAssessment(Assessment assessment);
        Task<Assessment> GetAssessmentById(Guid id);
        Task<ICollection<Assessment>> GetPaginatedAssessment(int pageNumber, int pageSize);
    }
}
