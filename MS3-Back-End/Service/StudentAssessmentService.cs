using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            return studentAssessments;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllEvaluatedAssessments()
        {
            var nonEvaluateAssessments = await _repository.GetAllEvaluatedAssessments();
            return nonEvaluateAssessments;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments()
        {
            var nonEvaluateAssessments = await _repository.GetAllNonEvaluateAssessments();
            return nonEvaluateAssessments;
        }

        public async Task<string> AddStudentAssessment(StudentAssessmentRequestDTO request)
        {
            var studentAssessment = new StudentAssessment
            {
                DateSubmitted = DateTime.Now,
                StudentAssessmentStatus = StudentAssessmentStatus.Completed,
                StudentId = request.StudentId,
                AssessmentId = request.AssessmentId
            };
            var studentAssessmentData = await _repository.AddStudentAssessment(studentAssessment);

            return "Completed Assessment Successfully";
        }

        public async Task<string> EvaluateStudentAssessment(Guid id , EvaluationRequestDTO request)
        {
            var studentAssessmentData = await _repository.StudentAssessmentGetById(id);
            if(studentAssessmentData == null)
            {
                throw new Exception("Student Assessment not found");
            }
            var assessmentData = await _assessmentRepository.GetAssessmentById(studentAssessmentData.AssessmentId);
            if(assessmentData == null)
            {
                throw new Exception("Assessment not found");
            }

            if (request.MarksObtaines < 0 || request.MarksObtaines > assessmentData.TotalMarks)
            {
                throw new Exception("Invalid Marks");
            }

            studentAssessmentData.MarksObtaines = request.MarksObtaines;
            studentAssessmentData.Grade = request.MarksObtaines < assessmentData.PassMarks ? Grade.Fail : Grade.Pass;
            studentAssessmentData.FeedBack = request.FeedBack;
            studentAssessmentData.DateEvaluated = DateTime.Now;
            studentAssessmentData.StudentAssessmentStatus = StudentAssessmentStatus.Reviewed;

            var updatedData = await _repository.EvaluateStudentAssessment(studentAssessmentData);

            return "Assessment Evaluated Successfully";
        }
    }
}
