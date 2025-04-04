using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Calories.Class
{
    // Класс для работы с сервером
    internal class ServerClass
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public ServerClass()
        {
            _httpClient = new HttpClient();
            _apiUrl = "http://c33661db.beget.tech/api/CreateUser.php";
        }

        // Класс для запроса регистрации
        public class RegistrationRequest
        {
            public string Login { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int Age { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public bool Gender { get; set; }
            public string Activity { get; set; }
            public string Aim { get; set; }
            public double? Calories { get; set; }
            public double? Water { get; set; }
            public double? Fats { get; set; }
            public double? Proteins { get; set; }
            public double? Carbs { get; set; }
        }

        // Класс для ответа сервера
        public class RegistrationResponse
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public int UserId { get; set; }
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request)
        {
            try
            {
                // Сериализация запроса
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправка POST-запроса
                var response = await _httpClient.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                // Десериализация ответа
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseContent);

                if (result.Status == "error")
                    throw new Exception(result.Message);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
            }
        }
    }
}