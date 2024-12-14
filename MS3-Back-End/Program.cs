
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            builder.Services.AddScoped<SendMailService>();
            builder.Services.AddScoped<SendMailRepository>();
            builder.Services.AddScoped<EmailServiceProvider>();


            // Register EmailConfig
            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

            //Authentication
            builder.Services.AddScoped<IAuthRepository,AuthRepository>();
            builder.Services.AddScoped<IAuthService,AuthService>();

            //Address
            builder.Services.AddScoped<IAddressRepository,AddressRepository>();
            builder.Services.AddScoped<IAddressService,AddressServise>();

            //course
            builder.Services.AddScoped<ICourseRepository, CourseRepositoy>();
            builder.Services.AddScoped<ICourseService,CourseService>();

            //CourseSchedule
            builder.Services.AddScoped<ICourseScheduleRepository,CourseScheduleRepository>();
            builder.Services.AddScoped<ICourseScheduleService,CourseScheduleService>();
          
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

            //FeedBack
            builder.Services.AddScoped<IFeedbacksRepository, FeedbacksRepository>();
            builder.Services.AddScoped<IFeedbacksService, FeedbacksService>();

            //Otp
            builder.Services.AddScoped<IOtpRepository, OtpRepository>();
            builder.Services.AddScoped<IOtpService, OtpService>();

            // Ensure EmailConfig is available as a singleton if needed
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            builder.Services.AddScoped<ApiService>();

            // Add Quartz services
            builder.Services.AddQuartz(q =>
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
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // Add HTTP client for API calls
            builder.Services.AddHttpClient<ApiService>();




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


            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200" , "https://waymakers-front-end-f0fnexg3ete4e0gm.uksouth-01.azurewebsites.net") 
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
