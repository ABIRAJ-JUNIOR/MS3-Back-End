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

        //public async Task<PaymentOverview> GetPaymentOverview()
        //{
        //    var paymentOverview = await (from payment in _context.Payments
        //                                 join enrollment in _context.Enrollments
        //                                     on payment.EnrollmentId equals enrollment.Id into enrollmentGroup
        //                                 from enrollment in enrollmentGroup.DefaultIfEmpty()
        //                                 join courseSchedule in _context.CourseSchedules
        //                                     on enrollment.CourseScheduleId equals courseSchedule.Id into courseScheduleGroup
        //                                 from courseSchedule in courseScheduleGroup.DefaultIfEmpty()
        //                                 join course in _context.Courses
        //                                     on courseSchedule.CourseId equals course.Id into courseGroup
        //                                 from course in courseGroup.DefaultIfEmpty()
        //                                 select new PaymentOverview
        //                                 {
                                            
        //                                 }).ToListAsync();

        //    return paymentOverview;
        //}

    }
}
