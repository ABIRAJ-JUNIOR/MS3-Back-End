﻿using MS3_Back_End.DTOs.RequestDTOs.Feedbacks;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
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
            var datas = await _feedbacksRepository.GetTopFeetbacks();

            var retundatas = datas.Select(datas => new FeedbacksResponceDTO()
            {
                CourseId = datas.CourseId,
                FeedBackDate = datas.FeedBackDate,
                FeedBackText = datas.FeedBackText,
                Rating = datas.Rating,
                StudentId = datas.StudentId,
                Id = datas.Id,
                Student = datas.Student !=  null ? new StudentResponseDTO()
                {
                    Id = datas.Student.Id,
                    Nic = datas.Student.Nic,
                    FirstName = datas.Student.FirstName,
                    LastName = datas.Student.LastName,
                    DateOfBirth = datas.Student.DateOfBirth,
                    Gender = ((Gender)datas.Student.Gender).ToString(),
                    Phone = datas.Student.Phone,
                    ImageUrl = datas.Student.ImageUrl!,
                    CteatedDate = datas.Student.CteatedDate,
                    UpdatedDate = datas.Student.UpdatedDate,
                } : new StudentResponseDTO()
            }).ToList();

            return retundatas;
        }

    }
}
