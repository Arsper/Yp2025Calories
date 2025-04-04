using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calories.Class
{
    internal class UserRegistration
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

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

        public class RegistrationResponse
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public int UserId { get; set; }
        }

        public UserRegistration(string apiBaseUrl)
        {
            _httpClient = new HttpClient();
            _apiUrl = apiBaseUrl;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest request)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrEmpty(request.Login) || request.Login.Length < 5 || request.Login.Length > 16)
                    throw new ArgumentException("Логин должен быть от 5 до 16 символов");

                if (string.IsNullOrEmpty(request.Email) || !request.Email.Contains("@"))
                    throw new ArgumentException("Некорректный формат email");

                if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
                    throw new ArgumentException("Пароль должен содержать минимум 8 символов");

                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new RegistrationResponse
                    {
                        Status = "error",
                        Message = response.StatusCode == HttpStatusCode.Conflict
                            ? "Пользователь с таким email или логином уже существует"
                            : $"Ошибка сервера: {response.StatusCode}"
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseContent);

                if (result == null)
                    throw new Exception("Не удалось обработать ответ сервера");

                return result;
            }
            catch (Exception ex)
            {
                return new RegistrationResponse
                {
                    Status = "error",
                    Message = $"Ошибка регистрации: {ex.Message}"
                };
            }
        }
    }
}