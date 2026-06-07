using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Enums;
using FrontEnd.Models;
using FrontEnd.Models.Periodo;
using FrontEnd.Models.Tarea;
using FrontEnd.Models.Asignatura;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontEnd.Models.Recordatorio;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IPeriodoService _periodoService;
        private readonly ITareaService _tareaService;
        private readonly IAsignaturasService _asignaturaService;
        private readonly IRecordatoriosService _recordatorioService;

        public DashboardController(ILogger<DashboardController> logger,
                                IPeriodoService periodoService,
                                ITareaService tareaService,
                                IAsignaturasService asignaturasService,
                                IRecordatoriosService recordatorioService)
        {
            _logger = logger;
            _periodoService = periodoService;
            _tareaService = tareaService;
            _asignaturaService = asignaturasService;
            _recordatorioService = recordatorioService;
        }

        public async Task<IActionResult> Index()
        {
            var modelo = new DashboardViewModel();

            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

            var hoy = DateTime.Now;

            var periodoActual = periodos
                .FirstOrDefault(p => p.FechaInicio <= hoy && p.FechaFin >= hoy);

            var tareas = await _tareaService.GetTareasByUsuario(userId);

            foreach (var tarea in tareas)
            {
                tarea.Asignatura = await _asignaturaService
                    .GetAsignaturaByIdAsync(tarea.AsignaturaId);
            }

            var tareasPendientes = periodoActual == null
    ? new List<FrontEnd.Models.Tarea.Tarea>()
    : tareas
        .Where(t => t.Estado == (int)TareaEnum.Pendiente
                 && t.FechaEntrega >= periodoActual.FechaInicio
                 && t.FechaEntrega <= periodoActual.FechaFin)
        .OrderBy(t => t.FechaEntrega)
        .ToList();

            var recordatorios = await _recordatorioService.GetRecordatoriosByUsuarioIdAsync(userId);

            _logger.LogInformation($"Valor entero enum periodo: {(int)TareaEnum.Pendiente}");

            modelo.Periodo = periodoActual;
            modelo.Asignaturas = periodoActual?.Asignaturas ?? new List<FrontEnd.Models.Asignatura.Asignatura>();
            modelo.Tareas = tareasPendientes;
            modelo.Recordatorios = recordatorios;

            return View(modelo);
        }


    }
}