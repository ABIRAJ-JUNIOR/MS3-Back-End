using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs;

namespace MS3_Back_End.IService
{
    public interface IFeedbacksService
    {
        Task<FeedbacksResponceDTO> AddFeedbacks(FeedbacksRequestDTO reqfeedback);
        Task<ICollection<FeedbacksResponceDTO>> GetAllFeedbacks();
    }
}
