using MS3_Back_End.Common.Configuration;

namespace MS3_Back_End.Auto_API_Run
{
    /// <summary>
    /// Service for internal API calls used by scheduled jobs.
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ApiService(HttpClient httpClient, ApiSettings apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings;
            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl.TrimEnd('/') + "/");
        }

        public async Task ReminderAPI()
        {
            var response = await _httpClient.GetAsync("api/Payment/PaymentReminder");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment reminder API call succeeded.");
            }
            else
            {
                Console.WriteLine($"Payment reminder API call failed: {response.StatusCode}");
            }
        }

        public async Task AnnouncementExpiry()
        {
            var response = await _httpClient.GetAsync("api/Announcement/ValidAnnouncements");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Announcement expiry API call succeeded.");
            }
            else
            {
                Console.WriteLine($"Announcement expiry API call failed: {response.StatusCode}");
            }
        }
    }
}
