
using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using MS3_Back_End.Service;

namespace MS3_Back_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.WebHost.UseWebRoot("wwwroot");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            //Authentication
            builder.Services.AddScoped<IAuthRepository,AuthRepository>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            //Address
            builder.Services.AddScoped<IAddressRepository,AddressRepository>();
            builder.Services.AddScoped<IAddressService,AddressServise>();

            //course
            builder.Services.AddScoped<ICourseRepository, CourseRepositoy>();
            builder.Services.AddScoped<ICourseService,CourseService>();

            //CourseShedule
            builder.Services.AddScoped<ICourseSheduleRepository,CourseSheduleRepository>();
            builder.Services.AddScoped<ICourseSheduleService,CourseSheduleService>();
          
            //ContactUs
            builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
            builder.Services.AddScoped<IContactUsService, ContactUsService>();

            //CourseCategory
            builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();

            //Notification
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            //Assessment
            builder.Services.AddScoped<IAssessmentRepository,AssessmentRepository>();
            builder.Services.AddScoped<IAssessmentService,AssessmentService>();
          
            //enrollements 
            builder.Services.AddScoped<IEnrollmentRepository,EnrollmentRepository>();
            builder.Services.AddScoped<IEnrollementService, EnrollmentService>();

            //StudentAssessment
            builder.Services.AddScoped<IStudentAssessmentRepository, StudentAssessmentRepository>();
            builder.Services.AddScoped<IStudentAssessmentService, StudentAssessmentService>();

            //Announcement
            builder.Services.AddScoped<IAnnouncementRepository,AnnouncementRepository>();   
            builder.Services.AddScoped<IAnnouncementService,AnnouncementService>();

            //Payment
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
          
            //Student
            builder.Services.AddScoped<IStudentRepository,StudentRepository>();
            builder.Services.AddScoped<IStudentService,StudentService>();

            //Admin
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            //Audit Log
            builder.Services.AddScoped<IAuditLogRepository,AuditLogRepository>();
            builder.Services.AddScoped<IAuditLogService,AuditLogService>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
