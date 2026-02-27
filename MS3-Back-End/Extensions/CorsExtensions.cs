namespace MS3_Back_End.Extensions
{
    /// <summary>
    /// Extension methods for configuring CORS policies.
    /// </summary>
    public static class CorsExtensions
    {
        public const string DefaultPolicyName = "AllowSpecificOrigins";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                ?? new[] { "http://localhost:4200" };

            services.AddCors(options =>
            {
                options.AddPolicy(DefaultPolicyName, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                });
            });

            return services;
        }
    }
}
