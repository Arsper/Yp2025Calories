using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calories.Class
{
    internal class UserRegistrationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public class CheckUserRequest
        {
            public string Login { get; set; }
            public string Email { get; set; }
        }

        public class ApiResponse
        {
            public string Status { get; set; }
            public string Message { get; set; }
        }

        public UserRegistrationService(string apiBaseUrl)
        {
            _httpClient = new HttpClient();
            _apiUrl = apiBaseUrl;
        }

        // Основной метод для проверки пользователя
        public async Task<ApiResponse> CheckUserExistsAsync(string login, string email)
        {
            try
            {
                var requestData = new { login, email };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        Status = "error",
                        Message = $"Ошибка сервера: {response.StatusCode}"
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Status = "error",
                    Message = $"Ошибка подключения: {ex.Message}"
                };
            }
        }

        // Приватный метод для отправки запроса
        private async Task<ApiResponse> SendApiRequest(CheckUserRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse>(responseContent);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка HTTP запроса: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Ошибка обработки JSON: {ex.Message}");
            }
        }
    }
}
