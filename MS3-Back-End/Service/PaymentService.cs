using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponseDTO> CreatePayment(PaymentRequestDTO paymentRequest)
        {
            if(paymentRequest.AmountPaid < 0)
            {
                throw new Exception("Amount Should be positive");
            }

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                PaymentType = paymentRequest.PaymentType,
                PaymentMethod = paymentRequest.PaymentMethod,
                AmountPaid = paymentRequest.AmountPaid,
                PaymentDate = DateTime.Now,
                InstallmentNumber = paymentRequest.InstallmentNumber,
                EnrollmentId = paymentRequest.EnrollmentId
            };


            var createdPayment = await _paymentRepository.CreatePayment(payment);

            return new PaymentResponseDTO
            {
                Id = createdPayment.Id,
                PaymentType = ((PaymentTypes)createdPayment.PaymentType).ToString(),
                PaymentMethod = ((PaymentMethots)createdPayment.PaymentMethod).ToString(),
                AmountPaid = createdPayment.AmountPaid,
                PaymentDate = createdPayment.PaymentDate,
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
                InstallmentNumber = p.InstallmentNumber,
                EnrollmentId = p.EnrollmentId
            }).ToList();

            return response;
        }
    }
}
