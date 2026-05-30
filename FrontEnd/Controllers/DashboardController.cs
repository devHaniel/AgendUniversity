using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IPeriodoService _periodoService;

        public DashboardController(ILogger<DashboardController> logger,
                                IPeriodoService periodoService)
        {
            _logger = logger;
            _periodoService = periodoService;
        }

        public async Task<IActionResult> Index()
        {
            var modelo = new DashboardViewModel();
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var ultimoPeriodo = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

            var hoy = DateTime.Now;
            var periodoActual = ultimoPeriodo.FirstOrDefault(p => p.FechaInicio <= hoy && p.FechaFin >= hoy);
            if (periodoActual != null)
            {_logger.LogInformation($"Periodo actual encontrado: {periodoActual.Nombre} (ID: {periodoActual.Id})");}
            else
            {_logger.LogInformation($"No se encontró un periodo actual para el usuario ID: {userId}");} 

            modelo.Periodo = periodoActual;
            modelo.Asignaturas = modelo.Periodo?.Asignaturas ?? new List<Asignatura>();
            
            return View(modelo);
        }
        
        
    }
}