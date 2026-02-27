namespace MS3_Back_End.Common.Configuration
{
    /// <summary>
    /// Configuration for internal API calls (e.g., scheduled jobs).
    /// </summary>
    public class ApiSettings
    {
        public const string SectionName = "ApiBaseUrl";

        /// <summary>
        /// Base URL for internal API calls. Used by scheduled jobs.
        /// </summary>
        public string BaseUrl { get; set; } = "https://localhost:7044";
    }
}
