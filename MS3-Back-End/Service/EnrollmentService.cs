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
        private readonly IPaymentService _paymentService;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseScheduleRepository courseScheduleRepository, IPaymentService paymentService)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseScheduleRepository = courseScheduleRepository;
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

            var Payment = new List<Payment>()
            {
                new Payment()
                {
                    PaymentType = EnrollmentReq.PaymentRequest.PaymentType,
                    PaymentMethod = EnrollmentReq.PaymentRequest.PaymentMethod,
                    AmountPaid = EnrollmentReq.PaymentRequest.AmountPaid,
                    PaymentDate = DateTime.Now,
                    InstallmentNumber = EnrollmentReq.PaymentRequest.InstallmentNumber != null ? EnrollmentReq.PaymentRequest.InstallmentNumber:null,
                    ImagePath = EnrollmentReq.PaymentRequest.ImageUrl != null ? EnrollmentReq.PaymentRequest.ImageUrl  : string.Empty,
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
                    ImagePath = payment.ImagePath,
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
                    ImagePath = payment.ImagePath,
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
                    ImagePath = payment.ImagePath,
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
                    ImagePath = payment.ImagePath,
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
