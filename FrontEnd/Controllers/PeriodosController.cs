using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class PeriodosController : Controller
    {
        private readonly ILogger<PeriodosController> _logger;
        private readonly IPeriodoService _periodoService;

        public PeriodosController(ILogger<PeriodosController> logger,
                                IPeriodoService periodoService)
        {
            _logger = logger;
            _periodoService = periodoService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            _logger.LogInformation($"Obteniendo periodos para usuario ID: {userId}");
            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);
            var ordenados = periodos.OrderByDescending(p => p.FechaInicio).ToList();
            return View(ordenados);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeriodoCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            dto.UsuarioId = userId;

            await _periodoService.CreatePeriodoAsync(dto);
            TempData["Mensaje"] = "Periodo creado exitosamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Implementar lógica para mostrar el formulario de edición
            var periodo = await _periodoService.GetPeriodoByIdAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }
            return View(periodo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PeriodoUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var success = await _periodoService.EditPeriodoAsync(id, dto);
            if (!success)
            {
                ModelState.AddModelError("", "Error al actualizar el periodo.");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var periodo = await _periodoService.GetPeriodoByIdAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }
            var success = await _periodoService.DeletePeriodoAsync(id);
            if (!success)
            {
                ModelState.AddModelError("", "Error al eliminar el periodo.");
                return View(periodo);
            }
            return RedirectToAction(nameof(Index));
        }



    }
}