using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MS3_Back_End.Auto_API_Run;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.Email;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using MS3_Back_End.Service;
using Quartz;
using System.Text;

namespace MS3_Back_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigurePipeline(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            // Register EmailConfig
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            // Register services
            RegisterServices(builder.Services);

            // Add Quartz services
            ConfigureQuartz(builder.Services);

            // Add HTTP client for API calls
            builder.Services.AddHttpClient<ApiService>();

            // Configure JWT authentication
            ConfigureAuthentication(builder);

            // Configure CORS
            ConfigureCors(builder);

            // Configure Swagger
            ConfigureSwagger(builder);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SendMailService>();
            services.AddScoped<SendMailRepository>();
            services.AddScoped<EmailServiceProvider>();

            // Authentication
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            // Address
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, AddressService>();

            // Course
            services.AddScoped<ICourseRepository, CourseRepositoy>();
            services.AddScoped<ICourseService, CourseService>();

            // CourseSchedule
            services.AddScoped<ICourseScheduleRepository, CourseScheduleRepository>();
            services.AddScoped<ICourseScheduleService, CourseScheduleService>();

            // ContactUs
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();

            // CourseCategory
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();

            // Notification
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            // Assessment
            services.AddScoped<IAssessmentRepository, AssessmentRepository>();
            services.AddScoped<IAssessmentService, AssessmentService>();

            // Enrollments
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollementService, EnrollmentService>();

            // StudentAssessment
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

            services.AddScoped<ApiService>();
        }

        private static void ConfigureQuartz(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Define a job
                var jobKey = new JobKey("DailyApiJob");
                q.AddJob<ApiJob>(opts => opts.WithIdentity(jobKey));

                // Schedule the job to run daily at 8:00 AM
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("DailyApiTrigger")
                    .WithCronSchedule("0 0 8 * * ?"));
            });

            // Register Quartz as a hosted service
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

        private static void ConfigureCors(WebApplicationBuilder builder)
        {
            var corsPolicyName = "AllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                    });
            });
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigins");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}