using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                FeedBackDate = DateTime.Now,
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

        public async Task<ICollection<FeedbacksResponceDTO>> GetAllFeedbacks()
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

        public async Task<ICollection<FeedbacksResponceDTO>> GetTopFeetbacks()
        {
            var data = await _feedbacksRepository.GetTopFeetbacks();

            var retundatas = data.Select(data => new FeedbacksResponceDTO()
            {
                CourseId = data.CourseId,
                FeedBackDate = data.FeedBackDate,
                FeedBackText = data.FeedBackText,
                Rating = data.Rating,
                StudentId = data.StudentId,
                Id = data.Id,
                Student = data.Student !=  null ? new StudentResponseDTO()
                {
                    Id = data.Student.Id,
                    Nic = data.Student.Nic,
                    FirstName = data.Student.FirstName,
                    LastName = data.Student.LastName,
                    DateOfBirth = data.Student.DateOfBirth,
                    Gender = ((Gender)data.Student.Gender).ToString(),
                    Phone = data.Student.Phone,
                    ImageUrl = data.Student.ImageUrl!,
                    CreatedDate = data.Student.CreatedDate,
                    UpdatedDate = data.Student.UpdatedDate,
                } : new StudentResponseDTO()
            }).ToList();

            return retundatas;
        }

        public async Task<ICollection<FeedbacksResponceDTO>> GetFeedBacksByStudentId(Guid Id)
        {
            var data = await _feedbacksRepository.GetFeedBacksByStudentId(Id);
            var retundatas = data.Select(data => new FeedbacksResponceDTO()
            {
                CourseId = data.CourseId,
                FeedBackDate = data.FeedBackDate,
                FeedBackText = data.FeedBackText,
                Rating = data.Rating,
                StudentId = data.StudentId,
                Id = data.Id,
                Student = null,
            }).ToList();

            return retundatas;
        }


        public async Task<PaginationResponseDTO<PaginatedFeedbackResponseDTO>> GetPaginatedFeedBack(int pageNumber, int pageSize)
        {
            var feedbacks = await _feedbacksRepository.GetPaginatedFeedBack(pageNumber, pageSize);
            var allFeedbacks = await _feedbacksRepository.getAllFeedbacks();

            var retundatas = feedbacks.Select(data => new PaginatedFeedbackResponseDTO()
            {
                CourseId = data.CourseId,
                FeedBackDate = data.FeedBackDate,
                FeedBackText = data.FeedBackText,
                Rating = data.Rating,
                StudentId = data.StudentId,
                Id = data.Id,
                Course = data.Course != null ? new CourseResponseDTO()
                {
                    Id = data.Course.Id,
                    CourseCategoryId = data.Course.CourseCategoryId,
                    CourseName = data.Course.CourseName,
                    Level = ((CourseLevel)data.Course.Level).ToString(),
                    CourseFee = data.Course.CourseFee,
                    Description = data.Course.Description,
                    Prerequisites = data.Course.Prerequisites,
                    ImageUrl = data.Course.ImageUrl!,
                    CreatedDate = data.Course.CreatedDate,
                    UpdatedDate = data.Course.UpdatedDate,
                } : new CourseResponseDTO(),
                Student = data.Student != null ? new StudentResponseDTO()
                {
                    Id = data.Student.Id,
                    Nic = data.Student.Nic,
                    FirstName = data.Student.FirstName,
                    LastName = data.Student.LastName,
                    DateOfBirth = data.Student.DateOfBirth,
                    Gender = ((Gender)data.Student.Gender).ToString(),
                    Phone = data.Student.Phone,
                    ImageUrl = data.Student.ImageUrl!,
                    CreatedDate = data.Student.CreatedDate,
                    UpdatedDate = data.Student.UpdatedDate,
                } : new StudentResponseDTO()
            }).ToList();

            var paginationResponseDto = new PaginationResponseDTO<PaginatedFeedbackResponseDTO>
            {
                Items = retundatas,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allFeedbacks.Count / (double)pageSize),
                TotalItem = allFeedbacks.Count,
            };

            return paginationResponseDto;
        }
    }
}
