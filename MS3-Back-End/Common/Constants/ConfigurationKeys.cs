namespace MS3_Back_End.Common.Constants
{
    /// <summary>
    /// Configuration section and key names to avoid magic strings.
    /// </summary>
    public static class ConfigurationKeys
    {
        public static class ConnectionStrings
        {
            public const string DbConnection = "DBConnection";
        }

        public static class Sections
        {
            public const string Jwt = "Jwt";
            public const string EmailConfig = "EmailConfig";
            public const string Cors = "Cors";
            public const string ApiBaseUrl = "ApiBaseUrl";
        }
    }
}
