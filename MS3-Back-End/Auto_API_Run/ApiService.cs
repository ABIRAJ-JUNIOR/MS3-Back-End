namespace MS3_Back_End.Auto_API_Run
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ReminderAPI()
        {
            var response = await _httpClient.GetAsync("https://localhost:7044/api/Payment/PaymentReminder");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("API call succeeded.");
            }
            else
            {
                Console.WriteLine("API call failed.");
            }
        }

        public async Task AnnouncementExpiry()
        {
            var response = await _httpClient.GetAsync("https://localhost:7044/api/Announcement/ValidAnouncements");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("API call succeeded.");
            }
            else
            {
                Console.WriteLine("API call failed.");
            }
        }
    }
}
