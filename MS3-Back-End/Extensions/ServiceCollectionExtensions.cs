using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MS3_Back_End.Auto_API_Run;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Email;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using MS3_Back_End.Service;
using Quartz;

namespace MS3_Back_End.Extensions
{
    /// <summary>
    /// Extension methods for configuring application services and dependency injection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

            // Configuration
            services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            // Core infrastructure
            services.AddScoped<SendMailService>();
            services.AddScoped<SendMailRepository>();
            services.AddScoped<EmailServiceProvider>();

            // Domain services
            RegisterDomainServices(services);

            return services;
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            // Authentication
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            // Address
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, AddressService>();

            // Course
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();

            // Course Schedule
            services.AddScoped<ICourseScheduleRepository, CourseScheduleRepository>();
            services.AddScoped<ICourseScheduleService, CourseScheduleService>();

            // Course Category
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();

            // Contact Us
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();

            // Notification
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            // Assessment
            services.AddScoped<IAssessmentRepository, AssessmentRepository>();
            services.AddScoped<IAssessmentService, AssessmentService>();

            // Enrollment
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            // Student Assessment
            services.AddScoped<IStudentAssessmentRepository, StudentAssessmentRepository>();
            services.AddScoped<IStudentAssessmentService, StudentAssessmentService>();

            // Announcement
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            // Student
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            // Admin
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            // Audit Log
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IAuditLogService, AuditLogService>();

            // Feedback
            services.AddScoped<IFeedbacksRepository, FeedbacksRepository>();
            services.AddScoped<IFeedbacksService, FeedbacksService>();

            // OTP
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IOtpService, OtpService>();
        }
    }
}
