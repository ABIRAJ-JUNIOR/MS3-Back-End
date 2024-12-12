using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;

namespace MS3_Back_End.IService
{
    public interface IFeedbacksService
    {
        Task<FeedbacksResponceDTO> AddFeedbacks(FeedbacksRequestDTO reqfeedback);
        Task<ICollection<FeedbacksResponceDTO>> GetAllFeedbacks();
        Task<ICollection<FeedbacksResponceDTO>> GetTopFeetbacks();
        Task<ICollection<FeedbacksResponceDTO>> GetFeedBacksByStudentId(Guid Id);
        Task<PaginationResponseDTO<FeedbacksResponceDTO>> GetPaginatedFeedBack(int pageNumber, int pageSize);
    }
}
