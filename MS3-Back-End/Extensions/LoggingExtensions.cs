using NLog.Web;

namespace MS3_Back_End.Extensions
{
    /// <summary>
    /// Extension methods for configuring application logging.
    /// </summary>
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddNLogLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseNLog();

            return builder;
        }
    }
}
