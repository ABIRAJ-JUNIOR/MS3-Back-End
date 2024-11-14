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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PaymentService(IPaymentRepository paymentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _paymentRepository = paymentRepository;
            _webHostEnvironment = webHostEnvironment;
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

            if (paymentRequest.ImageFile != null)
            {
                payment.ImagePath = await SaveImageFile(paymentRequest.ImageFile);
            }

            var createdPayment = await _paymentRepository.CreatePayment(payment);

            return new PaymentResponseDTO
            {
                Id = createdPayment.Id,
                PaymentType = createdPayment.PaymentType,
                PaymentMethod = createdPayment.PaymentMethod,
                AmountPaid = createdPayment.AmountPaid,
                PaymentDate = createdPayment.PaymentDate,
                ImagePath = createdPayment.ImagePath,
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
                PaymentType = p.PaymentType,
                PaymentMethod = p.PaymentMethod,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                ImagePath = p.ImagePath,
                InstallmentNumber = p.InstallmentNumber,
                EnrollmentId = p.EnrollmentId
            }).ToList();

            return response;
        }

        private async Task<string> SaveImageFile(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Payment");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/Payment/{fileName}";
        }

    }
}
