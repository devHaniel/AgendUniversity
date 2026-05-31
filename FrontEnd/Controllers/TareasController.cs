using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        private readonly ILogger<TareasController> _logger;
        private readonly ITareaService _tareaService;
        private readonly IAsignaturasService _asignaturaService;

        public TareasController(ILogger<TareasController> logger, ITareaService tareaService, IAsignaturasService asignaturaService)
        {
            _logger = logger;
            _tareaService = tareaService;
            _asignaturaService = asignaturaService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            _logger.LogInformation($"Obteniendo tareas para usuario ID: {userId}");
            var tareas = await _tareaService.GetTareasByUsuario(userId);

            foreach (var tarea in tareas)
            {
                tarea.Asignatura = await _asignaturaService
                    .GetAsignaturaByIdAsync(tarea.AsignaturaId);
            }

            var ordenados = tareas
                .OrderByDescending(t => t.FechaCreacion)
                .ToList();

            return View(ordenados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}