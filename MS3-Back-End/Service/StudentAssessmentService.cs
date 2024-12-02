using Microsoft.CodeAnalysis.CSharp.Syntax;
using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
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

        public async Task<List<StudentAssessmentResponseDTO>> GetStudentAssesmentById(Guid studentId)
        {
            var studentAssessments = await _repository.GetStudentAssesmentById(studentId);

            var response = studentAssessments.Select(item => new StudentAssessmentResponseDTO
            {
                Id = item.Id,
                StudentId = item.StudentId,
                MarksObtaines = item.MarksObtaines,
                AssessmentId = item.AssessmentId,
                Grade = item.Grade?.ToString(),
                FeedBack = item.FeedBack,
                DateEvaluated = item.DateEvaluated,
                DateSubmitted = item.DateSubmitted,
                StudentAssessmentStatus = item.StudentAssessmentStatus.ToString(),

                AssessmentResponse = item.Assessment != null ? new AssessmentResponseDTO
                {
                    Id = item.Assessment.Id,
                    CourseId = item.Assessment.CourseId,
                    AssessmentTitle = item.Assessment.AssessmentTitle,

                    AssessmentType = item.Assessment.AssessmentType.ToString() ?? string.Empty,

                    StartDate = item.Assessment.StartDate,
                    EndDate = item.Assessment.EndDate,
                    TotalMarks = item.Assessment.TotalMarks,
                    PassMarks = item.Assessment.PassMarks,
                    AssessmentLink = item.Assessment.AssessmentLink,
                    CreatedDate = item.Assessment.CreatedDate,
                    UpdateDate = item.Assessment.UpdateDate,

                    AssessmentStatus = item.Assessment.Status.ToString() ?? string.Empty,

                    courseResponse = item.Assessment.Course != null ? new CourseResponseDTO
                    {
                        Id = item.Assessment.Course.Id,
                        CourseCategoryId = item.Assessment.Course.CourseCategoryId,
                        CourseName = item.Assessment.Course.CourseName,
                        Level = item.Assessment.Course.Level.ToString() ?? string.Empty,
                        CourseFee = item.Assessment.Course.CourseFee,
                        Description = item.Assessment.Course.Description,
                        Prerequisites = item.Assessment.Course.Prerequisites,
                        ImageUrl = item.Assessment.Course.ImageUrl,
                        CreatedDate = item.Assessment.Course.CreatedDate,
                        UpdatedDate = item.Assessment.Course.UpdatedDate
                    } : null

                } : null,

                StudentResponse = item.Student != null ? new StudentResponseDTO
                {
                    Id = item.Student.Id,
                    Nic = item.Student.Nic,
                    FirstName = item.Student.FirstName,
                    LastName = item.Student.LastName,
                    DateOfBirth = item.Student.DateOfBirth,
                    Gender = item.Student.Gender.ToString(),
                    Phone = item.Student.Phone,
                    ImageUrl = item.Student.ImageUrl,
                    UpdatedDate = item.Student.UpdatedDate,
                    IsActive = item.Student.IsActive
                } : null
            }).ToList();



            return response;

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
