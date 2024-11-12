﻿using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService(IAssessmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AssessmentResponseDTO> AddAssessment(AssessmentRequestDTO request)
        {
            var assessment = new Assessment()
            {
                CourseId = request.CourseId,
                AssessmentType = request.AssessmentType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalMarks = request.TotalMarks,
                PassMarks = request.PassMarks,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Status = AssessmentStatus.NotStarted,
            };

            var assessmentData = await _repository.AddAssessment(assessment);

            var response = new AssessmentResponseDTO()
            {
                Id = assessmentData.Id,
                CourseId = assessmentData.CourseId,
                AssessmentType = assessmentData.AssessmentType,
                StartDate = assessmentData.StartDate,
                EndDate = assessmentData.EndDate,
                TotalMarks = assessmentData.TotalMarks,
                PassMarks = assessmentData.PassMarks,
                CreatedDate = assessmentData.CreatedDate,
                UpdateDate = assessmentData.UpdateDate,
                Status = assessmentData.Status,
            };

            return response;
        }

        public async Task<ICollection<AssessmentResponseDTO>> GetAllAssessment()
        {
            var assessmentList = await _repository.GetAllAssessment();

            var responseList = new List<AssessmentResponseDTO>();

            foreach (var item in assessmentList)
            {
                var responseObj = new AssessmentResponseDTO()
                {
                    Id = item.Id,
                    CourseId = item.CourseId,
                    AssessmentType = item.AssessmentType,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    TotalMarks = item.TotalMarks,
                    PassMarks = item.PassMarks,
                    CreatedDate = item.CreatedDate,
                    UpdateDate = item.UpdateDate,
                    Status = item.Status,
                };
                responseList.Add(responseObj);
            }

            return responseList;
        }
    }
}
