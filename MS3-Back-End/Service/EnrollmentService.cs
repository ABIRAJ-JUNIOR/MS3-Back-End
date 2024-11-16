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
        private readonly ICourseSheduleRepository _courseSheduleRepository;
        private readonly IPaymentService _paymentService;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseSheduleRepository courseSheduleRepository, IPaymentService paymentService)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseSheduleRepository = courseSheduleRepository;
            _paymentService = paymentService;
        }

        public async Task<EnrollmentResponseDTO> AddEnrollment(EnrollmentRequestDTO EnrollmentReq)
        {
            var courseSheduleData = await _courseSheduleRepository.GetCourseSheduleById(EnrollmentReq.CourseSheduleId);
            if(courseSheduleData == null)
            {
                throw new Exception("CourseShedule not found");
            }

            if(courseSheduleData.MaxStudents == courseSheduleData.EnrollCount)
            {
                throw new Exception("Reach limit");
            }

            if(EnrollmentReq.PaymentRequest == null)
            {
                throw new Exception("Payment required");
            }

            courseSheduleData.EnrollCount = courseSheduleData.EnrollCount + 1;
            await _courseSheduleRepository.UpdateCourseShedule(courseSheduleData);

            var Payment = new List<Payment>()
            {
                new Payment()
                {
                    PaymentType = EnrollmentReq.PaymentRequest.PaymentType,
                    PaymentMethod = EnrollmentReq.PaymentRequest.PaymentMethod,
                    AmountPaid = EnrollmentReq.PaymentRequest.AmountPaid,
                    PaymentDate = DateTime.Now,
                    InstallmentNumber = EnrollmentReq.PaymentRequest.InstallmentNumber != null ? EnrollmentReq.PaymentRequest.InstallmentNumber:null,
                    ImagePath = EnrollmentReq.PaymentRequest.ImageFile != null ? await _paymentService.SaveImageFile(EnrollmentReq.PaymentRequest.ImageFile) : string.Empty,
                }
            };

            var Enrollment = new Enrollment()
            {
                StudentId = EnrollmentReq.StudentId,
                CourseSheduleId = EnrollmentReq.CourseSheduleId,
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
                CourseSheduleId = data.CourseSheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = data.PaymentStatus,
                IsActive = data.IsActive
            };

            if(data.Payments != null)
            {
                var PaymentResponse = data.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = payment.PaymentType,
                    PaymentMethod = payment.PaymentMethod,
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


        public async Task<List<EnrollmentResponseDTO>> SearchEnrollmentByUserId(Guid SearchUserId)
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
                CourseSheduleId = item.CourseSheduleId,
                EnrollmentDate = item.EnrollmentDate,
                PaymentStatus = item.PaymentStatus,
                IsActive = item.IsActive,
                PaymentResponse = item.Payments != null ? item.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = payment.PaymentType,
                    PaymentMethod = payment.PaymentMethod,
                    AmountPaid = payment.AmountPaid,
                    PaymentDate = payment.PaymentDate,
                    ImagePath = payment.ImagePath,
                    InstallmentNumber = payment.InstallmentNumber != null ? payment.InstallmentNumber : null,
                    EnrollmentId = payment.EnrollmentId
                }).ToList() : []
            }).ToList();    

            return ListEnrollment;
        }


        public async Task<List<EnrollmentResponseDTO>> GetAllEnrollements()
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
                CourseSheduleId = item.CourseSheduleId,
                EnrollmentDate = item.EnrollmentDate,
                PaymentStatus = item.PaymentStatus,
                IsActive = item.IsActive,
                PaymentResponse = item.Payments != null ? item.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = payment.PaymentType,
                    PaymentMethod = payment.PaymentMethod,
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
                CourseSheduleId = data.CourseSheduleId,
                EnrollmentDate = data.EnrollmentDate,
                PaymentStatus = data.PaymentStatus,
                IsActive = data.IsActive
            };

            if(data.Payments != null)
            {
                var PaymentResponse = data.Payments.Select(payment => new PaymentResponseDTO()
                {
                    Id = payment.Id,
                    PaymentType = payment.PaymentType,
                    PaymentMethod = payment.PaymentMethod,
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
