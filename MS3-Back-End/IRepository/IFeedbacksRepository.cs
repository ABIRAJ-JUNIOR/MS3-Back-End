using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IFeedbacksRepository
    {
        Task<Feedbacks> AddFeedbacks(Feedbacks feedbacks);
        Task<ICollection<Feedbacks>> getAllFeedbacks();
        Task<ICollection<Feedbacks>> GetTopFeetbacks();
        Task<ICollection<Feedbacks>> GetFeedBacksByStudentId(Guid Id);
        Task<ICollection<Feedbacks>> GetPaginatedFeedBack(int pageNumber, int pageSize);
    }
}
