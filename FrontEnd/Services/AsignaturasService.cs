using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Services.Interfaces;

namespace FrontEnd.Services
{
    public class AsignaturasService : IAsignaturasService
    {
        private readonly ILogger<AsignaturasService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AsignaturasService(ILogger<AsignaturasService> logger, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Asignatura>> GetAsignaturasByUsuarioIdAsync(int usuarioId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en AsignaturasService");
                return new List<Asignatura>();
            }

            var token = TokenHelper.ObtenerToken(httpContext);
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation($"Header Authorization establecido con token");
            }
            else
            {
                _logger.LogWarning("Token está vacío o nulo");
            }
            
            var response = await client.GetAsync($"api/asignaturas/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new List<Asignatura>();
            }

            return await response.Content.ReadFromJsonAsync<List<Asignatura>>();
        }

        public async Task<Asignatura> GetAsignaturaByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en AsignaturasService");
                return new Asignatura();
            }

            var token = TokenHelper.ObtenerToken(httpContext);
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation($"Header Authorization establecido con token");
            }
            else
            {
                _logger.LogWarning("Token está vacío o nulo");
            }
            
            var response = await client.GetAsync($"api/asignaturas/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new Asignatura();
            }

            return await response.Content.ReadFromJsonAsync<Asignatura>();
        }

        public async Task<Asignatura> CreateAsignaturaAsync(AsignaturaCreateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en AsignaturasService");
                return null;
            }

            var token = TokenHelper.ObtenerToken(httpContext);
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation($"Header Authorization establecido con token");
            }
            else
            {
                _logger.LogWarning("Token está vacío o nulo");
            }
            
            var response = await client.PostAsJsonAsync("api/asignaturas", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Asignatura>();
        }

        public async Task<bool> UpdateAsignaturaAsync(int id, AsignaturaUpdateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en AsignaturasService");
                return false;
            }

            var token = TokenHelper.ObtenerToken(httpContext);
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation($"Header Authorization establecido con token");
            }
            else
            {
                _logger.LogWarning("Token está vacío o nulo");
            }
            
            var response = await client.PutAsJsonAsync($"api/asignaturas/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return false;
            }

            return true;
        }


        public async Task<bool> DeleteAsignaturaAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en AsignaturasService");
                return false;
            }

            var token = TokenHelper.ObtenerToken(httpContext);
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                _logger.LogInformation($"Header Authorization establecido con token");
            }
            else
            {
                _logger.LogWarning("Token está vacío o nulo");
            }
            
            var response = await client.DeleteAsync($"api/asignaturas/{id}");

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