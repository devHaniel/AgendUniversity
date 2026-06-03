using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Helpers;
using FrontEnd.Models.Periodo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FrontEnd.Services.Interfaces
{
    public class PeriodoService : IPeriodoService
    {
        private readonly ILogger<PeriodoService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PeriodoService(ILogger<PeriodoService> logger, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Periodo>> GetPeriodosByUsuarioIdAsync(int usuarioId)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en PeriodoService");
                return new List<Periodo>();
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
            
            _logger.LogInformation($"Enviando solicitud GET a api/periodos/usuario/{usuarioId}");
            var response = await client.GetAsync($"api/periodos/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new List<Periodo>();
            }

            return await response.Content.ReadFromJsonAsync<List<Periodo>>();
        }

        public async Task<Periodo> GetPeriodoByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en PeriodoService");
                return new Periodo();
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
            
            var response = await client.GetAsync($"api/periodos/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return new Periodo();
            }

            return await response.Content.ReadFromJsonAsync<Periodo>();
        }


        public async Task<Periodo> CreatePeriodoAsync(PeriodoCreateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en PeriodoService");
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
            
            var response = await client.PostAsJsonAsync("api/periodos", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Periodo>();
        }

        public async Task<bool> EditPeriodoAsync(int id, PeriodoUpdateDto dto)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en PeriodoService");
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
            
            var response = await client.PutAsJsonAsync($"api/periodos/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Status: {response.StatusCode}");
                _logger.LogError($"Error API: {error}");
                return false;
            }

            return true; 
        }

        public async Task<bool> DeletePeriodoAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("BackEndApi");

            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HttpContext es nulo en PeriodoService");
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
            
            var response = await client.DeleteAsync($"api/periodos/{id}");

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