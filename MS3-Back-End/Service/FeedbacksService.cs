using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class FeedbacksService: IFeedbacksService
    {
        private readonly IFeedbacksRepository _feedbacksRepository;

        public FeedbacksService(IFeedbacksRepository feedbacksRepository)
        {
            _feedbacksRepository = feedbacksRepository;
        }
        public async Task<FeedbacksResponceDTO> AddFeedbacks(FeedbacksRequestDTO reqfeedback) 
        {
            var feedback = new Feedbacks()
            {
                StudentId = reqfeedback.StudentId,
                FeedBackDate = reqfeedback.FeedBackDate,
                FeedBackText = reqfeedback.FeedBackText,
                Rating = reqfeedback.Rating,
                CourseId = reqfeedback.CourseId,

            };
            var data= await _feedbacksRepository.AddFeedbacks(feedback);


            var returndata = new FeedbacksResponceDTO()
            {
                CourseId = data.CourseId,
                FeedBackText = data.FeedBackText,
                Rating = data.Rating,
                FeedBackDate = data.FeedBackDate,
                Id = data.Id,
                StudentId = data.StudentId,

            };
            return returndata;


        
        }
        public async Task<List<FeedbacksResponceDTO>> GetAllFeedbacks()
        {
            var datas= await _feedbacksRepository.getAllFeedbacks();

            var retundatas=datas.Select(datas=> new FeedbacksResponceDTO()
            {
                CourseId=datas.CourseId,
                FeedBackDate=datas.FeedBackDate,
                FeedBackText=datas.FeedBackText,
                Rating=datas.Rating,
                StudentId=datas.StudentId,
                Id=datas.Id,
            }).ToList();

            return retundatas;  
        }

    }
}
