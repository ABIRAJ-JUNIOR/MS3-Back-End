using Microsoft.Extensions.Options;
using MS3_Back_End.Auto_API_Run;
using MS3_Back_End.Common.Configuration;

namespace MS3_Back_End.Extensions
{
    /// <summary>
    /// Extension methods for configuring HttpClient and API services.
    /// </summary>
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddApiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection(ApiSettings.SectionName));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApiSettings>>().Value);

            services.AddHttpClient<ApiService>();

            return services;
        }
    }
}
