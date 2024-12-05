using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.RequestDTOs.Ènrollment;
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


  If you have any questions, feel free to contact us at <a href=""mailto:noreply.way.makers@gmail.com"">noreply.way.makers@gmail.com</a> or call <b>0702274212</b>.<br><br>

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


        public async Task<ICollection<EnrollmentResponseDTO>> SearchEnrollmentByUserId(Guid SearchUserId)
        {
            var data = await _enrollmentRepository.SearchEnrollments(SearchUserId);
            if (data == null)
            {
                throw new Exception("Search Not Found");
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
