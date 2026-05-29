using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services.Interfaces;

namespace FrontEnd.Services
{
    public class AuthApiService : IAuthApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AuthResponseModel> LoginAsync(LoginViewModel dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var response = await client.PostAsJsonAsync("auth/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Error API: {error}");

                return null;
            }

            return await response.Content.ReadFromJsonAsync<AuthResponseModel>();
        }

        public async Task<AuthResponseModel> RegisterAsync(RegisterViewModel dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var response = await client.PostAsJsonAsync("auth/register", dto);

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Error al registrar usuario");

            return await response.Content.ReadFromJsonAsync<AuthResponseModel>();
        }
    }
}