using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDBContext _context;

        public PaymentRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            var paymentData = await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return paymentData.Entity;
        }

        public async Task<ICollection<Payment>> GetAllPayments()
        {
            var paymentLists = await _context.Payments.ToListAsync();
            return paymentLists;
        }

        public async Task<ICollection<Payment>> RecentPayments()
        {
            var recentPayments = await _context.Payments.OrderByDescending(p => p.PaymentDate).Take(5).ToListAsync();
            return recentPayments;
        }

        public async Task<PaymentOverview> GetPaymentOverview()
        {
            var paymentOverview = new PaymentOverview();

            paymentOverview.TotalAmount = await _context.Payments
                .SumAsync(p => (decimal)p.AmountPaid);

            paymentOverview.FullPayment = await _context.Payments
                .Where(p => p.PaymentType == PaymentTypes.FullPayment)
                .SumAsync(p => (decimal)p.AmountPaid);

            paymentOverview.Installment = await _context.Payments
                .Where(p => p.PaymentType == PaymentTypes.Installment)
                .SumAsync(p => (decimal)p.AmountPaid);

            var overdueAmounts = await _context.Enrollments
                .Where(e => e.PaymentStatus == PaymentStatus.InProcess)
                .Select(e => new
                {
                    courseFee = e.CourseSchedule!.Course!.CourseFee,
                    TotalPaid = _context.Payments
                    .Where(p => p.EnrollmentId == e.Id)
                    .Sum(p => (decimal?)p.AmountPaid) ?? 0
                })
                .ToListAsync();

            paymentOverview.OverDue = overdueAmounts
                .Sum(e => e.courseFee - e.TotalPaid);

            return paymentOverview;
        }

    }
}
