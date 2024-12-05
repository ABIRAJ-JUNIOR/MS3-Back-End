using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class StudentAssessmentService : IStudentAssessmentService
    {
        private readonly IStudentAssessmentRepository _repository;
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly INotificationRepository _notificationRepository;

        public StudentAssessmentService(IStudentAssessmentRepository repository, IAssessmentRepository assessmentRepository, ICourseRepository courseRepository, IStudentRepository studentRepository, INotificationRepository notificationRepository)
        {
            _repository = repository;
            _assessmentRepository = assessmentRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _notificationRepository = notificationRepository;
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
                        ImageUrl = item.Assessment.Course.ImageUrl!,
                        CreatedDate = item.Assessment.Course.CreatedDate,
                        UpdatedDate = item.Assessment.Course.UpdatedDate
                    } : new CourseResponseDTO()

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
                    ImageUrl = item.Student.ImageUrl!,
                    UpdatedDate = item.Student.UpdatedDate,
                    IsActive = item.Student.IsActive
                } : null
            }).ToList();

            return response;

        }
        
        public async Task<StudentAssessmentResponseDTO> EvaluateStudentAssessment(Guid id , EvaluationRequestDTO request)
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
            var courseData = await _courseRepository.GetCourseById(assessmentData.CourseId);
            var studentData = await _studentRepository.GetStudentById(updatedData.StudentId);


            string NotificationMessage = $@"

<b>Subject:</b> 🏆 Your Course Assessment Results<br><br>

Dear {studentData.FirstName} {studentData.LastName},<br><br>

Congratulations! Your results for the Assessment <b>{assessmentData.AssessmentTitle}</b> are now available. Here's a summary:<br><br>

<b>Score:</b> {updatedData.MarksObtaines}<br>
<b>Grade:</b> {updatedData.Grade}<br>
<b>Completion Date:</b> {updatedData.DateEvaluated:MM/dd/yyyy}

You have successfully completed the assessment and are one step closer to achieving your learning goals!<br><br>

If you need any feedback or have questions, feel free to contact us at <a href='mailto:noreply.way.makers@gmail.com'>noreply.way.makers@gmail.com</a> or call <b>0702274212</b>.<br><br>

Best regards,<br>
Way Makers

";

            var Message = new Notification
            {
                Message = NotificationMessage,
                NotificationType = NotificationType.Results,
                StudentId = updatedData.StudentId,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(Message);

            var response = new StudentAssessmentResponseDTO()
            {
                Id = updatedData.Id,
                MarksObtaines = updatedData.MarksObtaines,
                Grade = updatedData.Grade != null ? ((Grade)updatedData.Grade).ToString() : null,
                FeedBack = updatedData.FeedBack,
                DateEvaluated = updatedData.DateEvaluated,
                DateSubmitted = updatedData.DateSubmitted,
                StudentAssessmentStatus = ((StudentAssessmentStatus)updatedData.StudentAssessmentStatus).ToString(),
                StudentId = updatedData.StudentId,
                AssessmentId = updatedData.AssessmentId,
            };

            return response;
        }


        public async Task<PaginationResponseDTO<StudentAssessmentResponseDTO>> PaginationGetByStudentID(Guid studentId, int pageNumber, int pageSize)
        {
            var students = await _repository.GetStudentAssesmentById(studentId);

            var response = await _repository.PaginationGetByStudentID(studentId, pageNumber, pageSize);

            var responseList = response.Select(item => new StudentAssessmentResponseDTO
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

                    AssessmentStatus = item.Assessment.Status.ToString() ?? string.Empty

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
                    ImageUrl = item.Student.ImageUrl!,
                    UpdatedDate = item.Student.UpdatedDate,
                    IsActive = item.Student.IsActive
                } : null
            }).ToList();

            return new PaginationResponseDTO<StudentAssessmentResponseDTO>
            {
                Items = responseList,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(students.Count() / (double)pageSize),
                TotalItem = students.Count(),
            };
        }


    }
}
