using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Repository
{
    public class StudentAssessmentRepository : IStudentAssessmentRepository
    {
        private readonly AppDBContext _dbcontext;

        public StudentAssessmentRepository(AppDBContext context)
        {
            _dbcontext = context;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllAssessments()
        {
            var assessmentList = await (from studentAssessment in _dbcontext.StudentAssessments
                                        join assessment in _dbcontext.Assessments
                                            on studentAssessment.AssessmentId equals assessment.Id into assessmentGroup
                                        from assessment in assessmentGroup.DefaultIfEmpty()
                                        join course in _dbcontext.Courses
                                            on assessment.CourseId equals course.Id into courseGroup
                                        from course in courseGroup.DefaultIfEmpty()
                                        join student in _dbcontext.Students
                                            on studentAssessment.StudentId equals student.Id into studentGroup
                                        from student in studentGroup.DefaultIfEmpty()
                                        select new StudentAssessmentResponseDTO()
                                        {
                                            Id = studentAssessment.Id,
                                            MarksObtaines = studentAssessment.MarksObtaines,
                                            Grade = studentAssessment.Grade != null ? ((Grade)studentAssessment.Grade!).ToString() : null,
                                            FeedBack = studentAssessment.FeedBack,
                                            DateEvaluated = studentAssessment.DateEvaluated,
                                            DateSubmitted = studentAssessment.DateSubmitted,
                                            StudentAssessmentStatus = ((StudentAssessmentStatus)studentAssessment.StudentAssessmentStatus).ToString(),
                                            StudentId = studentAssessment.StudentId,
                                            AssessmentId = studentAssessment.AssessmentId,
                                            AssessmentResponse = assessment != null ? new AssessmentResponseDTO()
                                            {
                                                Id = assessment.Id,
                                                CourseId = assessment.CourseId,
                                                AssessmentTitle = assessment.AssessmentTitle,
                                                AssessmentType = ((AssessmentType)assessment.AssessmentType).ToString(),
                                                StartDate = assessment.StartDate,
                                                EndDate = assessment.EndDate,
                                                TotalMarks = assessment.TotalMarks,
                                                PassMarks = assessment.PassMarks,
                                                AssessmentLink = assessment.AssessmentLink,
                                                CreatedDate = assessment.CreatedDate,
                                                UpdateDate = assessment.UpdateDate,
                                                AssessmentStatus = ((AssessmentStatus)assessment.Status).ToString(),
                                                studentAssessmentResponses = null!,
                                                courseResponse = course != null ? new CourseResponseDTO()
                                                {
                                                    Id = course.Id,
                                                    CourseCategoryId = course.CourseCategoryId,
                                                    CourseName = course.CourseName,
                                                    Level = ((CourseLevel)course.Level).ToString(),
                                                    CourseFee = course.CourseFee,
                                                    Description = course.Description,
                                                    Prerequisites = course.Prerequisites,
                                                    ImageUrl = course.ImageUrl!,
                                                    CreatedDate = course.CreatedDate,
                                                    UpdatedDate = course.UpdatedDate,
                                                    Schedules = null,
                                                    Feedbacks = null,
                                                    AssessmentResponse = null,
                                                } : new CourseResponseDTO(),
                                            } : new AssessmentResponseDTO(),
                                            StudentResponse = student != null ? new StudentResponseDTO()
                                            {
                                                Id = student.Id,
                                                Nic = student.Nic,
                                                FirstName = student.FirstName,
                                                LastName = student.LastName,
                                                DateOfBirth = student.DateOfBirth,
                                                Gender = ((Gender)student.Gender).ToString(),
                                                Phone = student.Phone,
                                                ImageUrl = null!,
                                                CteatedDate = student.CteatedDate,
                                                UpdatedDate = student.UpdatedDate,
                                                Address = null!,
                                            } : new StudentResponseDTO(),
                                        }).ToListAsync();

            return assessmentList;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllEvaluatedAssessments()
        {
            var assessmentList = await (from studentAssessment in _dbcontext.StudentAssessments
                                        join assessment in _dbcontext.Assessments
                                            on studentAssessment.AssessmentId equals assessment.Id into assessmentGroup
                                        from assessment in assessmentGroup.DefaultIfEmpty()
                                        join course in _dbcontext.Courses
                                            on assessment.CourseId equals course.Id into courseGroup
                                        from course in courseGroup.DefaultIfEmpty()
                                        join student in _dbcontext.Students
                                            on studentAssessment.StudentId equals student.Id into studentGroup
                                        from student in studentGroup.DefaultIfEmpty()
                                        where studentAssessment.StudentAssessmentStatus == StudentAssessmentStatus.Reviewed
                                        select new StudentAssessmentResponseDTO()
                                        {
                                            Id = studentAssessment.Id,
                                            MarksObtaines = studentAssessment.MarksObtaines,
                                            Grade = studentAssessment.Grade != null ? ((Grade)studentAssessment.Grade!).ToString() : null,
                                            FeedBack = studentAssessment.FeedBack,
                                            DateEvaluated = studentAssessment.DateEvaluated,
                                            DateSubmitted = studentAssessment.DateSubmitted,
                                            StudentAssessmentStatus = ((StudentAssessmentStatus)studentAssessment.StudentAssessmentStatus).ToString(),
                                            StudentId = studentAssessment.StudentId,
                                            AssessmentId = studentAssessment.AssessmentId,
                                            AssessmentResponse = assessment != null ? new AssessmentResponseDTO()
                                            {
                                                Id = assessment.Id,
                                                CourseId = assessment.CourseId,
                                                AssessmentTitle = assessment.AssessmentTitle,
                                                AssessmentType = ((AssessmentType)assessment.AssessmentType).ToString(),
                                                StartDate = assessment.StartDate,
                                                EndDate = assessment.EndDate,
                                                TotalMarks = assessment.TotalMarks,
                                                PassMarks = assessment.PassMarks,
                                                AssessmentLink = assessment.AssessmentLink,
                                                CreatedDate = assessment.CreatedDate,
                                                UpdateDate = assessment.UpdateDate,
                                                AssessmentStatus = ((AssessmentStatus)assessment.Status).ToString(),
                                                studentAssessmentResponses = null!,
                                                courseResponse = course != null ? new CourseResponseDTO()
                                                {
                                                    Id = course.Id,
                                                    CourseCategoryId = course.CourseCategoryId,
                                                    CourseName = course.CourseName,
                                                    Level = ((CourseLevel)course.Level).ToString(),
                                                    CourseFee = course.CourseFee,
                                                    Description = course.Description,
                                                    Prerequisites = course.Prerequisites,
                                                    ImageUrl = course.ImageUrl!,
                                                    CreatedDate = course.CreatedDate,
                                                    UpdatedDate = course.UpdatedDate,
                                                    Schedules = null,
                                                    Feedbacks = null,
                                                    AssessmentResponse = null,
                                                } : new CourseResponseDTO(),
                                            } : new AssessmentResponseDTO(),
                                            StudentResponse = student != null ? new StudentResponseDTO()
                                            {
                                                Id = student.Id,
                                                Nic = student.Nic,
                                                FirstName = student.FirstName,
                                                LastName = student.LastName,
                                                DateOfBirth = student.DateOfBirth,
                                                Gender = ((Gender)student.Gender).ToString(),
                                                Phone = student.Phone,
                                                ImageUrl = null!,
                                                CteatedDate = student.CteatedDate,
                                                UpdatedDate = student.UpdatedDate,
                                                Address = null!,
                                            } : new StudentResponseDTO(),
                                        }).ToListAsync();
            return assessmentList;
        }

        public async Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments()
        {
            var assessmentList = await (from studentAssessment in _dbcontext.StudentAssessments
                                        join assessment in _dbcontext.Assessments
                                            on studentAssessment.AssessmentId equals assessment.Id into assessmentGroup
                                        from assessment in assessmentGroup.DefaultIfEmpty()
                                        join course in _dbcontext.Courses
                                            on assessment.CourseId equals course.Id into courseGroup
                                        from course in courseGroup.DefaultIfEmpty()
                                        join student in _dbcontext.Students
                                            on studentAssessment.StudentId equals student.Id into studentGroup
                                        from student in studentGroup.DefaultIfEmpty()
                                        where studentAssessment.StudentAssessmentStatus != StudentAssessmentStatus.Reviewed
                                        select new StudentAssessmentResponseDTO()
                                        {
                                            Id = studentAssessment.Id,
                                            MarksObtaines = studentAssessment.MarksObtaines,
                                            Grade = studentAssessment.Grade != null ? ((Grade)studentAssessment.Grade!).ToString() : null,
                                            FeedBack = studentAssessment.FeedBack,
                                            DateEvaluated = studentAssessment.DateEvaluated,
                                            DateSubmitted = studentAssessment.DateSubmitted,
                                            StudentAssessmentStatus = ((StudentAssessmentStatus)studentAssessment.StudentAssessmentStatus).ToString(),
                                            StudentId = studentAssessment.StudentId,
                                            AssessmentId = studentAssessment.AssessmentId,
                                            AssessmentResponse = assessment != null ? new AssessmentResponseDTO()
                                            {
                                                Id = assessment.Id,
                                                CourseId = assessment.CourseId,
                                                AssessmentTitle = assessment.AssessmentTitle,
                                                AssessmentType = ((AssessmentType)assessment.AssessmentType).ToString(),
                                                StartDate = assessment.StartDate,
                                                EndDate = assessment.EndDate,
                                                TotalMarks = assessment.TotalMarks,
                                                PassMarks = assessment.PassMarks,
                                                AssessmentLink = assessment.AssessmentLink,
                                                CreatedDate = assessment.CreatedDate,
                                                UpdateDate = assessment.UpdateDate,
                                                AssessmentStatus = ((AssessmentStatus)assessment.Status).ToString(),
                                                studentAssessmentResponses = null!,
                                                courseResponse = course != null ? new CourseResponseDTO()
                                                {
                                                    Id = course.Id,
                                                    CourseCategoryId = course.CourseCategoryId,
                                                    CourseName = course.CourseName,
                                                    Level = ((CourseLevel)course.Level).ToString(),
                                                    CourseFee = course.CourseFee,
                                                    Description = course.Description,
                                                    Prerequisites = course.Prerequisites,
                                                    ImageUrl = course.ImageUrl!,
                                                    CreatedDate = course.CreatedDate,
                                                    UpdatedDate = course.UpdatedDate,
                                                    Schedules = null,
                                                    Feedbacks = null,
                                                    AssessmentResponse = null,
                                                } : new CourseResponseDTO(),
                                            } : new AssessmentResponseDTO(),
                                            StudentResponse = student != null ? new StudentResponseDTO()
                                            {
                                                Id = student.Id,
                                                Nic = student.Nic,
                                                FirstName = student.FirstName,
                                                LastName = student.LastName,
                                                DateOfBirth = student.DateOfBirth,
                                                Gender = ((Gender)student.Gender).ToString(),
                                                Phone = student.Phone,
                                                ImageUrl = null!,
                                                CteatedDate = student.CteatedDate,
                                                UpdatedDate = student.UpdatedDate,
                                                Address = null!,
                                            } : new StudentResponseDTO(),
                                        }).ToListAsync();
            return assessmentList;
        }

        public async Task<StudentAssessment> AddStudentAssessment(StudentAssessment studentAssessment)
        {
            var assessmentData = await _dbcontext.StudentAssessments.AddAsync(studentAssessment);
            await _dbcontext.SaveChangesAsync();
            return assessmentData.Entity;
        }

        public async Task<StudentAssessment> EvaluateStudentAssessment(StudentAssessment studentAssessment)
        {
            var updatedData =  _dbcontext.StudentAssessments.Update(studentAssessment);
            await _dbcontext.SaveChangesAsync();
            return updatedData.Entity;
        }

        public async Task<StudentAssessment> StudentAssessmentGetById(Guid id)
        {
            var stusentAssessmentData = await _dbcontext.StudentAssessments.SingleOrDefaultAsync(sa => sa.Id == id);
            return stusentAssessmentData!;
        }

    }
}
