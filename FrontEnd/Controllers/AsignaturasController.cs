using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Enums;
using FrontEnd.Models;
using FrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class AsignaturasController : Controller
    {
        private readonly ILogger<AsignaturasController> _logger;
        private readonly IPeriodoService _periodoService;
        private readonly IAsignaturasService _asignaturasService;

        public AsignaturasController(ILogger<AsignaturasController> logger, IPeriodoService periodoService, IAsignaturasService asignaturasService)
        {
            _logger = logger;
            _periodoService = periodoService;
            _asignaturasService = asignaturasService;
        }

        public async Task<IActionResult> Index(int periodoId = 0, bool todos = false)
        {
            AsignaturaViewModel modelo;
            var userId = int.Parse(User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            ViewBag.Todos = todos;

            // mandaron a llamar un periodo específico
            if (periodoId != 0)
            {
                modelo = await GetAsignaturasByPeriodoAsync(userId, periodoId);
            }
            // mandaron a llamar todas las asignaturas
            else if (todos)
            {
                modelo = await GetTodosAsync(userId);
            }
            // no mandaron nada, se muestra el periodo actual
            else
            {
                var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);
                var hoy = DateTime.Now;

                var periodoActual = periodos
                    .FirstOrDefault(p => p.FechaInicio <= hoy && p.FechaFin >= hoy);

                if (periodoActual == null)
                {
                    modelo = new AsignaturaViewModel
                    {
                        Periodo = null,
                        Asignaturas = new List<Asignatura>()
                    };
                }
                else
                {
                    modelo = await GetAsignaturasByPeriodoAsync(userId, periodoActual.Id);
                }
            }

            modelo.Periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

            return View(modelo);
        }

        private async Task<AsignaturaViewModel> GetAsignaturasByPeriodoAsync(int usuarioId, int periodoId)
        {
            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(usuarioId);
            var periodo = periodos.FirstOrDefault(p => p.Id == periodoId);

            var model = new AsignaturaViewModel
            {
                Periodo = periodo,
                Asignaturas = periodo.Asignaturas ?? new List<Asignatura>()
            };

            return model;
        }

        private async Task<AsignaturaViewModel> GetTodosAsync(int usuarioId)
        {
            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(usuarioId);

            return new AsignaturaViewModel
            {
                Periodo = null,
                Asignaturas = periodos.SelectMany(p => p.Asignaturas).ToList()
            };
        }

        public async Task<IActionResult> Details(int asignaturaId)
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var asignatura = await _asignaturasService.GetAsignaturaByIdAsync(asignaturaId);

            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }

        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

            var model = new AsignaturaCreateViewModel
            {
                Periodos = periodos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList(),
                Estados = Enum.GetValues<AsignaturaEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AsignaturaCreateDto model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Aquí iría la lógica para crear la asignatura usando el servicio
            // await _asignaturasService.CreateAsignaturaAsync(...);\n            var result = await _asignaturasService.CreateAsignaturaAsync(model);
            var result = await _asignaturasService.CreateAsignaturaAsync(model);
            
            if (result is not null)
            {
                TempData["Mensaje"] = "Asignatura creada exitosamente";
                
                // Si viene de una URL de retorno, redirigir allá
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

            var asignatura = periodos
                .SelectMany(p => p.Asignaturas)
                .FirstOrDefault(a => a.Id == id);

            if (asignatura == null)
            {
                return NotFound();
            }

            var model = new AsignaturaEditViewModel
            {
                Id = asignatura.Id,
                Nombre = asignatura.Nombre,
                PeriodoId = asignatura.PeriodoId,
                Estado = asignatura.Estado,

                Periodos = periodos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList(),

                Estados = Enum.GetValues<AsignaturaEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AsignaturaEditViewModel model)
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (!ModelState.IsValid)
            {
                var periodos = await _periodoService.GetPeriodosByUsuarioIdAsync(userId);

                model.Periodos = periodos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList();

                model.Estados = Enum.GetValues<AsignaturaEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    })
                    .ToList();

                return View(model);
            }

            var asignatura = new AsignaturaUpdateDto
            {
                Nombre = model.Nombre,
                PeriodoId = model.PeriodoId,
                Estado = model.Estado
            };

            await _asignaturasService.UpdateAsignaturaAsync(model.Id, asignatura);

            TempData["Mensaje"] = "Asignatura editada correctamente";

            return RedirectToAction(nameof(Index), new { periodoId = model.PeriodoId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var asignatura = await _asignaturasService.GetAsignaturaByIdAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }
            var success = await _asignaturasService.DeleteAsignaturaAsync(id);
            if (!success)
            {
                ModelState.AddModelError("", "Error al eliminar la asignatura.");
                return View(asignatura);
            }
            TempData["Mensaje"] = "Asignatura eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}