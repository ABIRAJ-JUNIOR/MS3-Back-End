﻿using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class StudentAssessmentService : IStudentAssessmentService
    {
        private readonly IStudentAssessmentRepository _repository;

        public StudentAssessmentService(IStudentAssessmentRepository studentAssessmentRepository)
        {
            _repository = studentAssessmentRepository;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllAssessments()
        {
            var studentAssessments = await _repository.GetAllAssessments();
            var response = studentAssessments.Select(sa => new StudentAssessmentResponseDTO()
            {
                Id = sa.Id,
                MarksObtaines = sa.MarksObtaines,
                Grade = sa.Grade,
                FeedBack = sa.FeedBack,
                DateEvaluated = sa.DateEvaluated,
                StudentAssessmentStatus = sa.StudentAssessmentStatus,
                StudentId = sa.StudentId,
                AssesmentId = sa.AssesmentId
            }).ToList();

            return response;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllEvaluatedAssessments()
        {
            var nonEvaluateAssessments = await _repository.GetAllEvaluatedAssessments();
            var response = nonEvaluateAssessments.Select(sa => new StudentAssessmentResponseDTO()
            {
                Id = sa.Id,
                MarksObtaines = sa.MarksObtaines,
                Grade = sa.Grade,
                FeedBack = sa.FeedBack,
                DateEvaluated = sa.DateEvaluated,
                StudentAssessmentStatus = sa.StudentAssessmentStatus,
                StudentId = sa.StudentId,
                AssesmentId = sa.AssesmentId
            }).ToList();

            return response;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments()
        {
            var nonEvaluateAssessments = await _repository.GetAllNonEvaluateAssessments();
            var response = nonEvaluateAssessments.Select(sa => new StudentAssessmentResponseDTO()
            {
                Id = sa.Id,
                MarksObtaines = sa.MarksObtaines,
                Grade = sa.Grade,
                FeedBack = sa.FeedBack,
                DateEvaluated = sa.DateEvaluated,
                StudentAssessmentStatus = sa.StudentAssessmentStatus,
                StudentId = sa.StudentId,
                AssesmentId = sa.AssesmentId
            }).ToList();

            return response;
        }

        public async Task<string> AddStudentAssessment(StudentAssessmentRequestDTO request)
        {
            var studentAssessment = new StudentAssessment
            {
                DateSubmitted = request.DateSubmitted,
                StudentAssessmentStatus = request.StudentAssessmentStatus,
                StudentId = request.StudentId,
                AssesmentId = request.AssessmentId
            };
            var studentAssessmentData = await _repository.AddStudentAssessment(studentAssessment);

            return "Completed Assessment Successfully";
        }
    }
}
