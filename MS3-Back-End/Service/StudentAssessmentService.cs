using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
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
        private readonly IAssessmentRepository _assessmentRepository;

        public StudentAssessmentService(IStudentAssessmentRepository repository, IAssessmentRepository assessmentRepository)
        {
            _repository = repository;
            _assessmentRepository = assessmentRepository;
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
                DateSubmitted = DateTime.Now,
                StudentAssessmentStatus = request.StudentAssessmentStatus,
                StudentId = request.StudentId,
                AssesmentId = request.AssessmentId
            };
            var studentAssessmentData = await _repository.AddStudentAssessment(studentAssessment);

            return "Completed Assessment Successfully";
        }

        public async Task<string> EvaluateStudentAssessment(Guid id , EvaluationRequestDTO request)
        {
            var studentAssessmentData = await _repository.StudentAssessmentGetById(id);
        }
    }
}
