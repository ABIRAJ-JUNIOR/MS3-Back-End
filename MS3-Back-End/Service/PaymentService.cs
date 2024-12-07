using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly INotificationRepository _notificationRepository;

        public PaymentService(IPaymentRepository paymentRepository, IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository, ICourseScheduleRepository courseScheduleRepository, ICourseRepository courseRepository, INotificationRepository notificationRepository)
        {
            _paymentRepository = paymentRepository;
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseScheduleRepository = courseScheduleRepository;
            _courseRepository = courseRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<PaymentResponseDTO> CreatePayment(PaymentRequestDTO paymentRequest)
        {
            if(paymentRequest.AmountPaid < 0)
            {
                throw new Exception("Amount Should be positive");
            }

            if(paymentRequest.InstallmentNumber == 3)
            {
                var enrollmentData = await _enrollmentRepository.GetEnrollmentById(paymentRequest.EnrollmentId);
                enrollmentData.PaymentStatus = PaymentStatus.Paid;
                await _enrollmentRepository.UpdateEnrollment(enrollmentData);
            }

            var enrollmentDetails = await _enrollmentRepository.GetEnrollmentById(paymentRequest.EnrollmentId);
            if(enrollmentDetails == null)
            {
                throw new Exception("Enrollment Data No Found");
            }
            var courseScheduleData = await _courseScheduleRepository.GetCourseScheduleById(enrollmentDetails.CourseScheduleId);
            var courseData = await _courseRepository.GetCourseById(courseScheduleData.CourseId);
            var StudentData = await _studentRepository.GetStudentById(enrollmentDetails.StudentId);

            var today = DateTime.Now;
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                PaymentType = paymentRequest.PaymentType,
                PaymentMethod = paymentRequest.PaymentMethod,
                AmountPaid = paymentRequest.AmountPaid,
                PaymentDate = DateTime.Now,
                DueDate = paymentRequest.PaymentType == PaymentTypes.Installment && paymentRequest.InstallmentNumber != 3 ? CalculateInstallmentDueDate(today, courseScheduleData.Duration) : null,
                InstallmentNumber = paymentRequest.PaymentType == PaymentTypes.Installment ? paymentRequest.InstallmentNumber : null,
                EnrollmentId = paymentRequest.EnrollmentId
            };

            var createdPayment = await _paymentRepository.CreatePayment(payment);


            string NotificationMessage = $@"  <b>Subject:</b> 💳 Payment Confirmation<br><br>

  Dear {StudentData.FirstName} {StudentData.LastName},<br><br>

  We’re happy to confirm your payment for the course <b>{courseData.CourseName}</b> has been successfully processed!<br><br>

  <b>Amount:</b> {createdPayment.AmountPaid} Rs<br>
  <b>Payment Type:</b> {createdPayment.PaymentType} Rs<br>
  <b>Payment Date:</b> {createdPayment.PaymentDate}<br>
  <b>Transaction ID:</b> {createdPayment.Id}<br><br>

  You can now access your course materials and start your learning journey!<br><br>

  For any questions, feel free to contact us at <a href=""mailto:info.way.mmakers@gmail.com"">info.way.mmakers@gmail.com</a> or call <b>0702274212</b>.<br><br>

  Best regards,<br>
  Way Makers  
";

            var Message = new Notification
            {
                Message = NotificationMessage,
                NotificationType = NotificationType.Payment,
                StudentId = enrollmentDetails.StudentId,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(Message);

            return new PaymentResponseDTO
            {
                Id = createdPayment.Id,
                PaymentType = ((PaymentTypes)createdPayment.PaymentType).ToString(),
                PaymentMethod = ((PaymentMethots)createdPayment.PaymentMethod).ToString(),
                AmountPaid = createdPayment.AmountPaid,
                PaymentDate = createdPayment.PaymentDate,
                DueDate = createdPayment.DueDate,
                InstallmentNumber = createdPayment.InstallmentNumber,
                EnrollmentId = createdPayment.EnrollmentId
            };
        }

        public async Task<ICollection<PaymentResponseDTO>> GetAllPayments()
        {
            var paymentsList = await _paymentRepository.GetAllPayments();
            var response = paymentsList.Select(p => new PaymentResponseDTO()
            {
                Id = p.Id,
                PaymentType = ((PaymentTypes)p.PaymentType).ToString(),
                PaymentMethod = ((PaymentMethots)p.PaymentMethod).ToString(),
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                DueDate= p.DueDate,
                InstallmentNumber = p.InstallmentNumber,
                EnrollmentId = p.EnrollmentId
            }).ToList();

            return response;
        }

        public async Task<ICollection<PaymentResponseDTO>> RecentPayments()
        {
            var recentPayments = await _paymentRepository.RecentPayments();
            var response = recentPayments.Select(p => new PaymentResponseDTO()
            {
                Id = p.Id,
                PaymentType = ((PaymentTypes)p.PaymentType).ToString(),
                PaymentMethod = ((PaymentMethots)p.PaymentMethod).ToString(),
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                DueDate = p.DueDate,
                InstallmentNumber = p.InstallmentNumber,
                EnrollmentId = p.EnrollmentId
            }).ToList();

            return response;
        }

        public async Task<string> PaymentReminderSend()
        {
            var enrollments = await _enrollmentRepository.GetEnrollments();

            foreach (var enrollment in enrollments)
            {
                if(enrollment.PaymentStatus == PaymentStatus.InProcess)
                {
                    var payment = await _paymentRepository.GetLastPaymentOfEnrollment(enrollment.Id);
                    if (payment.PaymentType == PaymentTypes.Installment && payment.DueDate != null && payment.DueDate <= DateTime.UtcNow)
                    {
                        var studentData = await _studentRepository.GetStudentById(enrollment.StudentId);
                        var courseScheduleData = await _courseScheduleRepository.GetCourseScheduleById(enrollment.CourseScheduleId);
                        var courseData = await _courseRepository.GetCourseById(courseScheduleData.CourseId);
                        string NotificationMessage = $@"  <b>Subject:</b> 🔔 Payment Reminder<br><br>
                                                 Dear {studentData.FirstName} {studentData.LastName},<br><br>
                                                 This is a gentle reminder regarding your pending payment for the course <b>{courseData.CourseName}</b>.<br><br>
                                              <b>Outstanding Amount:</b> {payment.AmountPaid} Rs<br>
                                              <b>Due Date:</b> {payment.DueDate}<br>
                                              <b>Payment ID:</b> {payment.Id}<br><br>
                                                 Kindly make the payment before the due date to avoid any interruptions in your course access.<br><br>
                                              <b>Payment Instructions:</b><br>
                                                 You can complete your payment through our online portal or visit our office. If you have already made the payment, please disregard this message.<br><br>
                                                 For any questions, feel free to contact us at <a href=""mailto:info.way.mmakers@gmail.com"">info.way.mmakers@gmail.com</a> or call <b>0702274212</b>.<br><br>
                                                 Best regards,<br>
                                                 Way Makers  
                                           ";


                        var Message = new Notification
                        {
                            Message = NotificationMessage,
                            NotificationType = NotificationType.PaymentReminder,
                            StudentId = enrollment.StudentId,
                            DateSent = DateTime.Now,
                            IsRead = false
                        };

                        await _notificationRepository.AddNotification(Message);
                    }
                }
            }

            return "Reminder Send Successfull";

        }

        public async Task<PaymentOverview> GetPaymentOverview()
        {
            var paymentOverview = await _paymentRepository.GetPaymentOverview();
            return paymentOverview;
        }

        public DateTime CalculateInstallmentDueDate(DateTime paymentdate, int courseDuration)
        {
            return paymentdate.AddDays((courseDuration / 3));
        }

        public async Task<PaginationResponseDTO<PaymentFullDetails>> GetPaginatedPayments(int pageNumber, int pageSize)
        {
            var AllPayments = await _paymentRepository.GetAllPayments();

            var paginatedPayments = await _paymentRepository.GetPaginatedPayments(pageNumber, pageSize);

            var paginationResponseDto = new PaginationResponseDTO<PaymentFullDetails>
            {
                Items = paginatedPayments,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(AllPayments.Count / (double)pageSize),
                TotalItem = AllPayments.Count,
            };

            return paginationResponseDto;
        }
    }
}
