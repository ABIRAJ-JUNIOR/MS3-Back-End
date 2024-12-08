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

            paymentOverview.TotalPayment = await _context.Payments
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

        public async Task<ICollection<PaymentFullDetails>> GetPaginatedPayments(int pageNumber, int pageSize)
        {
            var paymentOverview = await (from payment in _context.Payments
                                         join enrollment in _context.Enrollments
                                             on payment.EnrollmentId equals enrollment.Id into enrollmentGroup
                                         from enrollment in enrollmentGroup.DefaultIfEmpty()

                                         join courseSchedule in _context.CourseSchedules
                                             on enrollment.CourseScheduleId equals courseSchedule.Id into courseScheduleGroup
                                         from courseSchedule in courseScheduleGroup.DefaultIfEmpty()

                                         join course in _context.Courses
                                             on courseSchedule.CourseId equals course.Id into courseGroup
                                         from course in courseGroup.DefaultIfEmpty()

                                         join student in _context.Students
                                             on enrollment.StudentId equals student.Id into studentGroup
                                         from student in studentGroup.DefaultIfEmpty()
                                         orderby payment.PaymentDate descending

                                         select new PaymentFullDetails
                                         {
                                             Id = payment.Id,
                                             StudentId = student.Id,
                                             StudentName = student.FirstName + student.LastName,
                                             CourseName = course.CourseName,
                                             AmountPaid = payment.AmountPaid,
                                             PaymentType = ((PaymentTypes)payment.PaymentType).ToString(),
                                             PaymentMethod = ((PaymentMethots)payment.PaymentMethod).ToString(),
                                             TransactionDate = payment.PaymentDate,
                                             DueDate = payment.DueDate != null ? payment.DueDate : null,
                                         })
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            return paymentOverview;

        }

        public async Task<Payment> GetLastPaymentOfEnrollment(Guid EnrollId)
        {
            var lastPayment = await _context.Payments.Where(p => p.EnrollmentId == EnrollId).OrderByDescending(p => p.PaymentDate).FirstOrDefaultAsync();
            return lastPayment!;
        }

    }
}
