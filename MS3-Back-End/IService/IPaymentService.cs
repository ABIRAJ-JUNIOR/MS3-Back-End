using MS3_Back_End.DTOs.RequestDTOs.Payment;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;

namespace MS3_Back_End.IService
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> CreatePayment(PaymentRequestDTO paymentRequest);
        Task<ICollection<PaymentResponseDTO>> GetAllPayments();
        Task<ICollection<PaymentResponseDTO>> RecentPayments();
        DateTime CalculateInstallmentDueDate(DateTime paymentdate, int courseDuration);
    }
}
