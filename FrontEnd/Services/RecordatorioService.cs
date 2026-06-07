using System.Net.Http.Headers;
using BackEnd.Dtos;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.Recordatorio;
using FrontEnd.Services.Interfaces;

namespace FrontEnd.Services
{
    public class RecordatorioService : IRecordatoriosService
    {
        private readonly ILogger<RecordatorioService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecordatorioService(
            ILogger<RecordatorioService> logger,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Recordatorio>> GetRecordatoriosByUsuarioIdAsync(int usuarioId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en RecordatoriosService");
                return new List<Recordatorio>();
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"api/recordatorios/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");

                return new List<Recordatorio>();
            }

            return await response.Content.ReadFromJsonAsync<List<Recordatorio>>()
                   ?? new List<Recordatorio>();
        }

        public async Task<Recordatorio> GetRecordatorioByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en RecordatoriosService");
                return new Recordatorio();
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"api/recordatorios/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");

                return new Recordatorio();
            }

            return await response.Content.ReadFromJsonAsync<Recordatorio>()
                   ?? new Recordatorio();
        }

        public async Task<Recordatorio> CreateRecordatorioAsync(
            RecordatorioCreateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en RecordatoriosService");
                return null;
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response =
                await client.PostAsJsonAsync("api/recordatorios", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");

                return null;
            }

            return await response.Content.ReadFromJsonAsync<Recordatorio>();
        }

        public async Task<bool> UpdateRecordatorioAsync(
            int id,
            RecordatorioUpdateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en RecordatoriosService");
                return false;
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response =
                await client.PutAsJsonAsync($"api/recordatorios/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");

                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRecordatorioAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en RecordatoriosService");
                return false;
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response =
                await client.DeleteAsync($"api/recordatorios/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");

                return false;
            }

            return true;
        }
    }
}