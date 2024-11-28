using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDBContext _Db;
        public StudentRepository(AppDBContext Db)
        {
            _Db = Db;
        }
        public async Task<Student> AddStudent(Student StudentReq)
        {
            try
            {
                var data = await _Db.Students.AddAsync(StudentReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while adding the student to the database.", ex);
            }
        }

        public async Task<ICollection<Student>> SearchStudent(string SearchText)
        {
            var data = await _Db.Students .Where(n => n.FirstName.Contains(SearchText) || 
               n.LastName!.Contains(SearchText) ||
               n.Nic.Contains(SearchText)).Include(a => a.Address).ToListAsync();
            return data;
        }

        public async Task<ICollection<Student>> GetAllStudente()
        {
            var data = await _Db.Students.Where(c => c.IsActive == true).Include(a => a.Address).ToListAsync();
            return data;
        }

        public async Task<Student> GetStudentById(Guid id)
        {
            var studentData = await _Db.Students.Include(a => a.Address).SingleOrDefaultAsync(s => s.Id == id);
            return studentData!;
        }
       
        public async Task<StudentFullDetailsResponseDTO> GetStudentFullDetailsById(Guid StudentId)
        {
            var data = await (from student in _Db.Students
                              join address in _Db.Addresses on student.Id equals address.StudentId into addressGroup
                              from address in addressGroup.DefaultIfEmpty()

                              join user in _Db.Users on student.Id equals user.Id into userGroup
                              from user in userGroup.DefaultIfEmpty()

                              join enrollment in _Db.Enrollments on student.Id equals enrollment.StudentId into enrollmentGroup
                              from enrollment in enrollmentGroup.DefaultIfEmpty()

                              join payment in _Db.Payments on enrollment.Id equals payment.EnrollmentId into paymentGroup
                              from payment in paymentGroup.DefaultIfEmpty()

                              join courseSchedule in _Db.CourseSchedules on enrollment.CourseScheduleId equals courseSchedule.Id into courseScheduleGroup
                              from courseSchedule in courseScheduleGroup.DefaultIfEmpty()

                              join course in _Db.Courses on courseSchedule.CourseId equals course.Id into courseGroup
                              from course in courseGroup.DefaultIfEmpty()

                              join studentAssessment in _Db.StudentAssessments on student.Id equals studentAssessment.StudentId into studentAssessmentGroup
                              from studentAssessment in studentAssessmentGroup.DefaultIfEmpty()

                              join assessment in _Db.Assessments on studentAssessment.AssessmentId equals assessment.Id into assessmentGroup
                              from assessment in assessmentGroup.DefaultIfEmpty()

                              where student.Id == StudentId && student.IsActive == true

                              select new StudentFullDetailsResponseDTO()
                              {
                                  Id = student.Id,
                                  Nic = student.Nic,
                                  FirstName = student.FirstName,
                                  LastName = student.LastName,
                                  DateOfBirth = student.DateOfBirth,
                                  Gender = ((Gender)student.Gender).ToString(),
                                  Phone = student.Phone,
                                  Email = user.Email,
                                  ImageUrl = student.ImageUrl!,
                                  CteatedDate = student.CteatedDate,
                                  UpdatedDate = student.UpdatedDate,
                                  Address = student.Address != null ? new AddressResponseDTO()
                                  {
                                      AddressLine1 = student.Address!.AddressLine1,
                                      AddressLine2 = student.Address!.AddressLine2,
                                      PostalCode = student.Address!.PostalCode,
                                      City = student.Address!.City,
                                      Country = student.Address!.Country,
                                      StudentId = student.Id,
                                  } : null,
                                  Enrollments = student.Enrollments != null ? student.Enrollments!.Select(enroll => new EnrollmentResponseDTO()
                                  {
                                      Id = enroll.Id,
                                      StudentId = enroll.StudentId,
                                      CourseScheduleId = enroll.CourseScheduleId,
                                      EnrollmentDate = enroll.EnrollmentDate,
                                      PaymentStatus = ((PaymentStatus)enroll.PaymentStatus).ToString(),
                                      IsActive = enroll.IsActive,
                                      PaymentResponse = enroll.Payments != null ? enroll.Payments.Select(payment => new PaymentResponseDTO()
                                      {
                                          Id = payment.Id,
                                          PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                                          PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                                          AmountPaid = payment.AmountPaid,
                                          PaymentDate = payment.PaymentDate,
                                          InstallmentNumber = payment.InstallmentNumber,
                                          EnrollmentId = payment.EnrollmentId
                                      }).ToList() : null,
                                      CourseScheduleResponse = enroll.CourseSchedule != null ? new CourseScheduleResponseDTO()
                                      {
                                          Id = enroll.CourseSchedule.Id,
                                          CourseId = enroll.CourseSchedule.CourseId,
                                          StartDate = enroll.CourseSchedule.StartDate,
                                          EndDate = enroll.CourseSchedule.EndDate,
                                          Duration = enroll.CourseSchedule.Duration,
                                          Time = enroll.CourseSchedule.Time,
                                          Location = enroll.CourseSchedule.Location,
                                          MaxStudents = enroll.CourseSchedule.MaxStudents,
                                          EnrollCount = enroll.CourseSchedule.EnrollCount,
                                          CreatedDate = enroll.CourseSchedule.CreatedDate,
                                          UpdatedDate = enroll.CourseSchedule.UpdatedDate,
                                          ScheduleStatus = ((ScheduleStatus)enroll.CourseSchedule.ScheduleStatus).ToString(),
                                          CourseResponse = enroll.CourseSchedule.Course != null ? new CourseResponseDTO()
                                          {
                                              Id = enroll.CourseSchedule.Course.Id,
                                              CourseCategoryId = enroll.CourseSchedule.Course.CourseCategoryId,
                                              CourseName = enroll.CourseSchedule.Course.CourseName,
                                              Level = ((CourseLevel)enroll.CourseSchedule.Course.Level).ToString(),
                                              CourseFee = enroll.CourseSchedule.Course.CourseFee,
                                              Description = enroll.CourseSchedule.Course.Description,
                                              Prerequisites = enroll.CourseSchedule.Course.Prerequisites,
                                              ImageUrl = enroll.CourseSchedule.Course.ImageUrl!,
                                              CreatedDate = enroll.CourseSchedule.Course.CreatedDate,
                                              UpdatedDate = enroll.CourseSchedule.Course.UpdatedDate,
                                          } : null,
                                      } : null
                                  }).ToList() : null,
                                  StudentAssessments = student.StudentAssessments != null ? student.StudentAssessments!.Select(sa => new StudentAssessmentResponseDTO()
                                  {
                                      Id = sa.Id,
                                      MarksObtaines = sa.MarksObtaines,
                                      Grade = sa.Grade != null ? ((Grade)sa.Grade).ToString() : null,
                                      FeedBack = sa.FeedBack,
                                      DateEvaluated = sa.DateEvaluated,
                                      DateSubmitted = sa.DateSubmitted,
                                      StudentAssessmentStatus = ((StudentAssessmentStatus)sa.StudentAssessmentStatus).ToString(),
                                      StudentId = sa.StudentId,
                                      AssessmentId = sa.AssessmentId,
                                      AssessmentResponse = sa.Assessment != null ? new AssessmentResponseDTO()
                                      {
                                          Id = sa.Assessment.Id,
                                          CourseId = sa.Assessment.CourseId,
                                          AssessmentTitle = sa.Assessment.AssessmentTitle,
                                          AssessmentType = ((AssessmentType)sa.Assessment.AssessmentType).ToString(),
                                          StartDate = sa.Assessment.StartDate,
                                          EndDate = sa.Assessment.EndDate,
                                          TotalMarks = sa.Assessment.TotalMarks,
                                          PassMarks = sa.Assessment.PassMarks,
                                          AssessmentLink = sa.Assessment.AssessmentLink,
                                          CreatedDate = sa.Assessment.CreatedDate,
                                          UpdateDate = sa.Assessment.UpdateDate,
                                          Status = ((AssessmentStatus)sa.Assessment.Status).ToString(),
                                          courseResponse = null!,
                                          studentAssessmentResponses = null!
                                      } : new AssessmentResponseDTO()
                                  }).ToList() : null,
                              }).FirstOrDefaultAsync();

            return data!;

        }

        public async Task<Student> UpdateStudent(Student Students)
        {
            var data = _Db.Students.Update(Students);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<string> DeleteStudent(Student Student)
        {
            _Db.Students.Update(Student);
            await _Db.SaveChangesAsync();
            return "Student Deleted Successfull";
        }

        public async Task<ICollection<StudentWithUserResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize)
        {
            var students = await (from student in _Db.Students
                                  join address in _Db.Addresses
                                    on student.Id equals address.StudentId into addressGroup
                                  from address in addressGroup.DefaultIfEmpty()
                                  join user in _Db.Users
                                    on student.Id equals user.Id into userGroup
                                  from user in userGroup.DefaultIfEmpty() 
                                  where student.IsActive != false
                                  orderby student.CteatedDate descending
                                  select new StudentWithUserResponseDTO
                                  {
                                      Id = student.Id,
                                      Nic = student.Nic,
                                      FirstName = student.FirstName,
                                      LastName = student.LastName,
                                      DateOfBirth = student.DateOfBirth,
                                      Gender = ((Gender)student.Gender).ToString(),
                                      Phone = student.Phone,
                                      Email = user.Email,
                                      ImageUrl = student.ImageUrl!,
                                      CteatedDate = student.CteatedDate,
                                      UpdatedDate = student.UpdatedDate,
                                      Address = address != null ? new AddressResponseDTO()
                                      {
                                          AddressLine1 = address.AddressLine1,
                                          AddressLine2 = address.AddressLine2,
                                          City = address.City,
                                          PostalCode = address.PostalCode,
                                          Country = address.Country,
                                          StudentId = address.Id
                                      } : null,
                                  })
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            return students;

        }
    }
}
