using System;
using System.Collections.Generic;
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
    public class CalendarioController : Controller
    {
        private readonly ILogger<CalendarioController> _logger;
        private readonly ITareaService _tareaService;
        private readonly IRecordatoriosService _recordatorioService;

        public CalendarioController(ILogger<CalendarioController> logger,
                                    ITareaService tareaService,
                                    IRecordatoriosService recordatorioService)
        {
            _logger = logger;
            _tareaService = tareaService;
            _recordatorioService = recordatorioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/calendario/eventos")]
        public async Task<IActionResult> GetEventos()
        {
            try
            {
                var userId = int.Parse(User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

                var tareas = await _tareaService.GetTareasByUsuario(userId);
                var recordatorios = await _recordatorioService.GetRecordatoriosByUsuarioIdAsync(userId);

                var eventos = new List<object>();

                // Agregar tareas como eventos
                foreach (var tarea in tareas)
                {
                    eventos.Add(new
                    {
                        id = $"tarea-{tarea.Id}",
                        title = $"Tarea: {tarea.Titulo}",
                        start = tarea.FechaEntrega,
                        backgroundColor = tarea.Estado == 0 ? "#dc3545" : "#28a745",
                        borderColor = tarea.Estado == 0 ? "#dc3545" : "#28a745",
                        description = tarea.Descripcion,
                        url = Url.Action("Details", "Tareas", new { id = tarea.Id })
                    });
                }

                // Agregar recordatorios como eventos
                foreach (var recordatorio in recordatorios)
                {
                    eventos.Add(new
                    {
                        id = $"recordatorio-{recordatorio.Id}",
                        title = $"Recordatorio: {recordatorio.Titulo}",
                        start = recordatorio.FechaRecordatorio,
                        backgroundColor = "#0d6efd",
                        borderColor = "#0d6efd",
                        description = recordatorio.Descripcion
                    });
                }

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener eventos del calendario: {ex.Message}");
                return BadRequest(new { message = "Error al cargar los eventos" });
            }
        }
    }
}
