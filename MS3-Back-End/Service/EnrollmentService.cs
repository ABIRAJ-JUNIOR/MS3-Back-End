using CloudinaryDotNet;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;

namespace MS3_Back_End.Service
{
    public class EnrollmentService :IEnrollementService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentService _paymentService;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseScheduleRepository courseScheduleRepository, INotificationRepository notificationRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, IPaymentService paymentService)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseScheduleRepository = courseScheduleRepository;
            _notificationRepository = notificationRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _paymentService = paymentService;
        }

        public async Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq)
        {
            var courseScheduleData = await _courseScheduleRepository.GetCourseScheduleById(EnrollmentReq.CourseScheduleId);
            if(courseScheduleData == null)
            {
                throw new Exception("CourseSchedule not found");
            }

            if(courseScheduleData.MaxStudents == courseScheduleData.EnrollCount)
            {
                throw new Exception("Reach limit");
            }

            if(EnrollmentReq.PaymentRequest == null)
            {
                throw new Exception("Payment required");
            }

            courseScheduleData.EnrollCount = courseScheduleData.EnrollCount + 1;
            await _courseScheduleRepository.UpdateCourseSchedule(courseScheduleData);

            var today = DateTime.Now;

            var Payment = new List<Payment>()
            {
                new Payment()
                {
                    PaymentType = EnrollmentReq.PaymentRequest.PaymentType,
                    PaymentMethod = EnrollmentReq.PaymentRequest.PaymentMethod,
                    AmountPaid = EnrollmentReq.PaymentRequest.AmountPaid,
                    PaymentDate = DateTime.Now,
                    DueDate = EnrollmentReq.PaymentRequest.PaymentType == PaymentTypes.Installment ? _paymentService.CalculateInstallmentDueDate(today, courseScheduleData.Duration) : null,
                    InstallmentNumber = EnrollmentReq.PaymentRequest.PaymentType == PaymentTypes.Installment ? EnrollmentReq.PaymentRequest.InstallmentNumber:null,
                }
            };

            var Enrollment = new Enrollment()
            {
                StudentId = EnrollmentReq.StudentId,
                CourseScheduleId = EnrollmentReq.CourseScheduleId,
                EnrollmentDate = DateTime.Now,
                PaymentStatus = EnrollmentReq.PaymentRequest.PaymentType == PaymentTypes.FullPayment ? PaymentStatus.Paid : PaymentStatus.InProcess,
                IsActive = true,
                Payments = Payment
            };

            var data = await _enrollmentRepository.AddEnrollment(Enrollment);
            var StudentData = await _studentRepository.GetStudentById(data.StudentId);
            var CourseScheduleData = await _courseScheduleRepository.GetCourseScheduleById(data.CourseScheduleId);
            var CourseData = await _courseRepository.GetCourseById(CourseScheduleData.CourseId); 

            string NotificationMessage = $@"
  <b>Subject:</b> 🎓 Course Enrollment Confirmation<br><br>

  Dear {StudentData.FirstName} {StudentData.LastName},<br><br>

  Congratulations! You have successfully enrolled in the course:<br><br>

  <b>Course Name:</b> {CourseData.CourseName}<br>
  📅 <b>Start Date:</b> {(CourseScheduleData.StartDate).ToString()}<br>
  ⏳ <b>Duration:</b> {CourseScheduleData.Duration} Days<br><br>

  We are excited to have you in this course and can't wait to see you excel! Here's what you need to do next:<br><br>

  1. Log in to your account <br>
  2. Check the course schedule and upcoming sessions.<br>
  3. Prepare yourself for an enriching learning journey.<br><br>


  If you have any questions, feel free to contact us at <a href=""mailto:info.way.mmakers@gmail.com"">info.way.mmakers@gmail.com</a> or call <b>0702274212</b>.<br><br>

  Welcome to the path of learning and success! 🎓<br><br>

  <b>Best regards,</b><br>
  Way Makers<br>
  Empowering learners, shaping futures.  
";

            var Message = new Notification
            {
                Message = NotificationMessage,
                NotificationType = NotificationType.Enrollment,
                StudentId = data.StudentId,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(Message);

            var EnrollmentResponse = new EnrollmentResponseDTO
            {
                Id=data.Id,
                StudentId = data.StudentId,
                CourseScheduleId = data.CourseScheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = ((PaymentStatus)data.PaymentStatus).ToString(),
                IsActive = data.IsActive
            };

            if(data.Payments != null)
            {
                var PaymentResponse = data.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                    PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                    AmountPaid = payment.AmountPaid,
                    PaymentDate = payment.PaymentDate,
                    DueDate = payment.DueDate,
                    InstallmentNumber = payment.InstallmentNumber != null ? payment.InstallmentNumber:null,
                    EnrollmentId = payment.EnrollmentId
                }).ToList();

                EnrollmentResponse.PaymentResponse = PaymentResponse;
            }

            return EnrollmentResponse;
        }


        public async Task<ICollection<EnrollmentResponseDTO>> GetEnrollmentsByStudentId(Guid studentId)
        {
            var data = await _enrollmentRepository.GetEnrollmentsByStudentId(studentId);
            if (data == null)
            {
                throw new Exception("Enrollments Not Found");
            }

            var response = data.Select( enrollment => new EnrollmentResponseDTO()
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
                CourseScheduleResponse = enrollment.CourseSchedule != null ? new CourseScheduleResponseDTO()
                {
                    Id = enrollment.CourseSchedule.Id,
                    CourseId = enrollment.CourseSchedule.CourseId,
                    StartDate = enrollment.CourseSchedule.StartDate,
                    EndDate = enrollment.CourseSchedule.EndDate,
                    Duration = enrollment.CourseSchedule.Duration,
                    Time = enrollment.CourseSchedule.Time,
                    Location = enrollment.CourseSchedule.Location,
                    MaxStudents = enrollment.CourseSchedule.MaxStudents,
                    EnrollCount = enrollment.CourseSchedule.EnrollCount,
                    CreatedDate = enrollment.CourseSchedule.CreatedDate,
                    UpdatedDate = enrollment.CourseSchedule.UpdatedDate,
                    ScheduleStatus = ((ScheduleStatus)enrollment.CourseSchedule.ScheduleStatus).ToString(),
                    CourseResponse = enrollment.CourseSchedule.Course != null ? new CourseResponseDTO()
                    {
                        Id = enrollment.CourseSchedule.Course.Id,
                        CourseCategoryId = enrollment.CourseSchedule.Course.CourseCategoryId,
                        CourseName = enrollment.CourseSchedule.Course.CourseName,
                        Level = ((CourseLevel)enrollment.CourseSchedule.Course.Level).ToString(),
                        CourseFee = enrollment.CourseSchedule.Course.CourseFee,
                        Description = enrollment.CourseSchedule.Course.Description,
                        Prerequisites = enrollment.CourseSchedule.Course.Prerequisites,
                        ImageUrl = enrollment.CourseSchedule.Course.ImageUrl!,
                        CreatedDate = enrollment.CourseSchedule.Course.CreatedDate,
                        UpdatedDate = enrollment.CourseSchedule.Course.UpdatedDate,
                        AssessmentResponse = enrollment.CourseSchedule.Course.Assessment != null ? enrollment.CourseSchedule.Course.Assessment.Select(a => new AssessmentResponseDTO()
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
            }).ToList();

            return response;
        }


        public async Task<ICollection<EnrollmentResponseDTO>> GetAllEnrollements()
        {
            var data = await _enrollmentRepository.GetEnrollments();
            if (data == null)
            {
                throw new Exception("Enrollment Not Available");
            }
            var ListEnrollment = data.Select(item => new EnrollmentResponseDTO()
            {
                Id = item.Id,
                StudentId = item.StudentId,
                CourseScheduleId = item.CourseScheduleId,
                EnrollmentDate = item.EnrollmentDate,
                PaymentStatus = ((PaymentStatus)item.PaymentStatus).ToString(),
                IsActive = item.IsActive,
                PaymentResponse = item.Payments != null ? item.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                    PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                    AmountPaid = payment.AmountPaid,
                    PaymentDate = payment.PaymentDate,
                    DueDate = payment.DueDate,
                    InstallmentNumber = payment.InstallmentNumber != null ? payment.InstallmentNumber : null,
                    EnrollmentId = payment.EnrollmentId
                }).ToList() : []

            }).ToList();

            return ListEnrollment;
        }

        public async Task<EnrollmentResponseDTO> GetEnrollmentId(Guid EnrollmentId)
        {
            var data = await _enrollmentRepository.GetEnrollmentById(EnrollmentId);
            if (data == null)
            {
                throw new Exception("Enrollment Not Found");
            }
            var EnrollmentResponse = new EnrollmentResponseDTO
            {
                Id = data.Id,
                StudentId = data.StudentId,
                CourseScheduleId = data.CourseScheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = ((PaymentStatus)data.PaymentStatus).ToString(),
                IsActive = data.IsActive
            };

            if(data.Payments != null)
            {
                var PaymentResponse = data.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                    PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                    AmountPaid = payment.AmountPaid,
                    PaymentDate = payment.PaymentDate,
                    DueDate = payment.DueDate,
                    InstallmentNumber = payment.InstallmentNumber != null ? payment.InstallmentNumber : null,
                    EnrollmentId = payment.EnrollmentId
                }).ToList();

                EnrollmentResponse.PaymentResponse = PaymentResponse;
            }

            return EnrollmentResponse;
        }

        public async Task<string> DeleteEnrollment(Guid Id)
        {
            var GetData = await _enrollmentRepository.GetEnrollmentById(Id);
            if (GetData == null)
            {
                throw new Exception("Course Id not Found");
            }
            GetData.IsActive = false;
            var data = await _enrollmentRepository.DeleteEnrollment(GetData);
            return data;
        }

    }
}
