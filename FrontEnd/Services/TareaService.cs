using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Helpers;
using FrontEnd.Models;
using FrontEnd.Models.Tarea;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Http;

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

        public async Task<PagedResult<Tarea>> GetTareasByUsuarioPaged(int usuarioId, int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return new PagedResult<Tarea>
                {
                    Page = page,
                    PageSize = pageSize
                };
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

            _logger.LogInformation($"Enviando solicitud GET a api/tareas/usuario/{usuarioId}/paged?page={page}&pageSize={pageSize}");
            var response = await client.GetAsync($"api/tareas/usuario/{usuarioId}/paged?page={page}&pageSize={pageSize}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new PagedResult<Tarea>
                {
                    Page = page,
                    PageSize = pageSize
                };
            }

            return await response.Content.ReadFromJsonAsync<PagedResult<Tarea>>() ?? new PagedResult<Tarea>();
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

        public async Task<bool> SubirArchivoAsync(Guid tareaId, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return false;

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
            }

            await using var stream = archivo.OpenReadStream();
            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(
                string.IsNullOrWhiteSpace(archivo.ContentType)
                    ? "application/octet-stream"
                    : archivo.ContentType);

            content.Add(fileContent, "archivo", archivo.FileName);

            var response = await client.PostAsync($"api/tareas/{tareaId}/archivos", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return false;
            }

            return true;
        }

        public async Task<TareaArchivoDownload> DescargarArchivoAsync(int archivoId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en TareaService");
                return null;
            }

            var token = TokenHelper.ObtenerToken(httpContext);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"api/tareas/archivos/{archivoId}/download");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return null;
            }

            var nombreOriginal = "archivo";
            var contentDisposition = response.Content.Headers.ContentDisposition;

            if (contentDisposition != null)
            {
                nombreOriginal = contentDisposition.FileNameStar
                    ?? contentDisposition.FileName?.Trim('"')
                    ?? nombreOriginal;
            }

            return new TareaArchivoDownload
            {
                Contenido = await response.Content.ReadAsByteArrayAsync(),
                ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream",
                NombreOriginal = nombreOriginal
            };
        }

        public async Task<bool> EliminarArchivoAsync(int archivoId)
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
            }

            var response = await client.DeleteAsync($"api/tareas/archivos/{archivoId}");

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
