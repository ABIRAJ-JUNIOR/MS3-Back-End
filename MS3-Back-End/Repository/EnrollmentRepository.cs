using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class EnrollmentRepository:IEnrollmentRepository
    {
        private readonly AppDBContext _Db;
        public EnrollmentRepository(AppDBContext db)
        {
            _Db = db;

        }

        public async Task<Enrollment> AddEnrollment(Enrollment Enrollment)
        {
                var data = await _Db.Enrollments.AddAsync(Enrollment);
                await _Db.SaveChangesAsync();
                return data.Entity;
        }
        public async Task<ICollection<EnrollmentResponseDTO>> GetEnrollmentsByStudentId(Guid studentId)
        {
            var data = await (from enrollment in _Db.Enrollments
                              join payment in _Db.Payments
                                  on enrollment.Id equals payment.EnrollmentId into paymentGroup
                              from payment in paymentGroup.DefaultIfEmpty()

                              join courseSchedule in _Db.CourseSchedules
                                  on enrollment.CourseScheduleId equals courseSchedule.Id into courseScheduleGroup
                              from courseSchedule in courseScheduleGroup.DefaultIfEmpty()

                              join course in _Db.Courses
                                  on courseSchedule.CourseId equals course.Id into courseGroup
                              from course in courseGroup.DefaultIfEmpty()

                              join assessment in _Db.Assessments
                                  on course.Id equals assessment.CourseId into assessmentGroup
                              from assessment in assessmentGroup.DefaultIfEmpty()
                              where enrollment.StudentId == studentId && enrollment.IsActive == true

                              select new EnrollmentResponseDTO()
                              {
                                  Id = enrollment.Id,
                                  StudentId = enrollment.StudentId,
                                  CourseScheduleId = enrollment.CourseScheduleId,
                                  EnrollmentDate = enrollment.EnrollmentDate,
                                  PaymentStatus = ((PaymentStatus)enrollment.PaymentStatus).ToString(),
                                  IsActive = enrollment.IsActive,
                                  PaymentResponse = enrollment.Payments != null ? enrollment.Payments!.Select(payment => new PaymentResponseDTO()
                                  {
                                      Id = payment.Id,
                                      PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                                      PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                                      AmountPaid = payment.AmountPaid,
                                      PaymentDate = payment.PaymentDate,
                                      DueDate = payment.DueDate,
                                      InstallmentNumber = payment.InstallmentNumber != null ? payment.InstallmentNumber : null,
                                      EnrollmentId = payment.EnrollmentId
                                  }).ToList() : null,
                                  CourseScheduleResponse = courseSchedule != null ? new CourseScheduleResponseDTO()
                                  {
                                      Id = courseSchedule.Id,
                                      CourseId = courseSchedule.CourseId,
                                      StartDate = courseSchedule.StartDate,
                                      EndDate = courseSchedule.EndDate,
                                      Duration = courseSchedule.Duration,
                                      Time = courseSchedule.Time,
                                      Location = courseSchedule.Location,
                                      MaxStudents = courseSchedule.MaxStudents,
                                      EnrollCount = courseSchedule.EnrollCount,
                                      CreatedDate = courseSchedule.CreatedDate,
                                      UpdatedDate = courseSchedule.UpdatedDate,
                                      ScheduleStatus = ((ScheduleStatus)courseSchedule.ScheduleStatus).ToString(),
                                      CourseResponse = course != null ? new CourseResponseDTO()
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
                                          AssessmentResponse = course.Assessment != null ? course.Assessment.Select(a => new AssessmentResponseDTO()
                                          {
                                              Id = a.Id,
                                              CourseId = a.CourseId,
                                              AssessmentTitle = a.AssessmentTitle,
                                              AssessmentType = ((AssessmentType)a.AssessmentType).ToString(),
                                              StartDate = a.StartDate,
                                              EndDate = a.EndDate,
                                              TotalMarks = a.TotalMarks,
                                              PassMarks = a.PassMarks,
                                              AssessmentLink = a.AssessmentLink,
                                              CreatedDate = a.CreatedDate,
                                              UpdateDate = a.UpdateDate,
                                              AssessmentStatus = ((AssessmentStatus)a.Status).ToString(),
                                              courseResponse = null!,
                                              studentAssessmentResponses = null!
                                          }).ToList() : null
                                      } : null,
                                  } : null
                              }).ToListAsync();

            return data;

        }

        public async Task<ICollection<Enrollment>> GetEnrollments()
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).Where(e => e.IsActive == true).ToListAsync();
            return data;
        }
        public async Task<Enrollment> GetEnrollmentById(Guid EnrollmentId)
        {
            var data = await _Db.Enrollments.Include(p => p.Payments).SingleOrDefaultAsync(c => c.Id == EnrollmentId);
            return data!;
        }

        public async Task<Enrollment> UpdateEnrollment(Enrollment Enrollment)
        {
            var updatedData = _Db.Update(Enrollment);
            await _Db.SaveChangesAsync();
            return updatedData.Entity;
        }

        public async Task<string> DeleteEnrollment(Enrollment course)
        {
            _Db.Enrollments.Update(course);
            await _Db.SaveChangesAsync();
            return "Enrollment IsActivate Successfull";
        }
    }
}
