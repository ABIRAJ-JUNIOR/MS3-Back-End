using MS3_Back_End.Extensions;
using NLog.Web;

namespace MS3_Back_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                .AddApplicationServices(builder.Configuration)
                .AddJwtAuthentication(builder.Configuration)
                .AddCorsPolicy(builder.Configuration)
                .AddSwaggerDocumentation()
                .AddQuartzJobs()
                .AddApiHttpClient(builder.Configuration);

            builder.AddNLogLogging();

            var app = builder.Build();

            // Configure pipeline
            app.UseApplicationPipeline(app.Environment);
            app.MapControllers();

            app.Run();
        }
    }
}
