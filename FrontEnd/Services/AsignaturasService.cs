using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
            
            _logger.LogInformation($"Enviando solicitud GET a api/asignaturas/usuario/{usuarioId}");
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

        public async Task<List<Asignatura>> GetAsignaturasByPeriodoIdAsync(int periodoId)
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

            _logger.LogInformation($"Enviando solicitud GET a api/asignaturas/periodo/{periodoId}");
            var response = await client.GetAsync($"api/asignaturas/periodo/{periodoId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new List<Asignatura>();
            }

            return await response.Content.ReadFromJsonAsync<List<Asignatura>>();
        }
    }
}