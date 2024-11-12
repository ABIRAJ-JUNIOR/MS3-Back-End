using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
