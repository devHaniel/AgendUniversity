using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Helpers;
using FrontEnd.Models.Tarea;
using FrontEnd.Services.Interfaces;

namespace FrontEnd.Services
{
    public class TareaService : ITareaService
    {
        private readonly ILogger<TareaService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TareaService(ILogger<TareaService> logger, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Tarea>> GetTareasByUsuario(int usuarioId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return new List<Tarea>();
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
            
            _logger.LogInformation($"Enviando solicitud GET a api/tareas/usuario/{usuarioId}");
            var response = await client.GetAsync($"api/tareas/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new List<Tarea>();
            }

            return await response.Content.ReadFromJsonAsync<List<Tarea>>();
        }

        public async Task<List<Tarea>> GetTareasByAsignatura(int asignaturaId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return new List<Tarea>();
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
            
            _logger.LogInformation($"Enviando solicitud GET a api/tareas/asignatura/{asignaturaId}");
            var response = await client.GetAsync($"api/tareas/asignatura/{asignaturaId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new List<Tarea>();
            }

            return await response.Content.ReadFromJsonAsync<List<Tarea>>();
        }
        public async Task<Tarea> GetTareasById(Guid tareaId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return new Tarea();
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
            
            _logger.LogInformation($"Enviando solicitud GET a api/tareas/{tareaId}");
            var response = await client.GetAsync($"api/tareas/{tareaId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new Tarea();
            }

            return await response.Content.ReadFromJsonAsync<Tarea>();
        }

        public async Task<Tarea> CreateTareaAsync(TareaCreateDto nuevaTarea)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return new Tarea();
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
            
            var response = await client.PostAsJsonAsync("api/tareas", nuevaTarea);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new Tarea();
            }

            return await response.Content.ReadFromJsonAsync<Tarea>();
        }

        public async Task<bool> EditTareaAsync(Guid tareaId, TareaUpdateDto tareaActualizada)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
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
            
            var response = await client.PutAsJsonAsync($"api/tareas/{tareaId}", tareaActualizada);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteTareaAsync(Guid tareaId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
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
            
            var response = await client.DeleteAsync($"api/tareas/{tareaId}");

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