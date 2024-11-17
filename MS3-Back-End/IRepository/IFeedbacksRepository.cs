using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IFeedbacksRepository
    {
        Task<Feedbacks> AddFeedbacks(Feedbacks feedbacks);

        Task<List<Feedbacks>> getAllFeedbacks();




    }
}
