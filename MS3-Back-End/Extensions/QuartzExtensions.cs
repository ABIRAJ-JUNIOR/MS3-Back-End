using MS3_Back_End.Auto_API_Run;
using Quartz;

namespace MS3_Back_End.Extensions
{
    /// <summary>
    /// Extension methods for configuring Quartz scheduled jobs.
    /// </summary>
    public static class QuartzExtensions
    {
        private const string DailyApiJobKey = "DailyApiJob";
        private const string DailyApiTriggerKey = "DailyApiTrigger";
        private const string DailyCronSchedule = "0 0 8 * * ?"; // 8:00 AM daily

        public static IServiceCollection AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(quartz =>
            {
                var jobKey = new JobKey(DailyApiJobKey);
                quartz.AddJob<ApiJob>(opts => opts.WithIdentity(jobKey));

                quartz.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity(DailyApiTriggerKey)
                    .WithCronSchedule(DailyCronSchedule));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
