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

        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            var asignaturas = await _asignaturaService.GetAsignaturasByUsuarioIdAsync(userId);

            var modelo = new TareaCreateViewModel
            {
                Asignaturas = asignaturas
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nombre
                    })
                    .ToList(),
                Estados = Enum.GetValues<TareaEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    })
                    .ToList()
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TareaCreateViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                await CargarListasTarea(model);
                return View(model);
            }

            var dto = new TareaCreateDto
            {
                AsignaturaId = model.AsignaturaId,
                Titulo = model.Titulo,
                Descripcion = model.Descripcion,
                Estado = model.Estado,
                FechaCreacion = model.FechaCreacion,
                FechaEntrega = model.FechaEntrega,
                Calificacion = model.Calificacion,
                Nota = model.Nota
            };

            var result = await _tareaService.CreateTareaAsync(dto);

            if (result is not null)
            {
                TempData["Mensaje"] = "Tarea creada exitosamente";

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }

            TempData["Mensaje"] = "No se creó la tarea.";

            await CargarListasTarea(model);
            return View(model);
        }

        private async Task CargarListasTarea(TareaCreateViewModel modelo)
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var asignaturas = await _asignaturaService.GetAsignaturasByUsuarioIdAsync(userId);

            modelo.Asignaturas = asignaturas
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Nombre
                })
                .ToList();

            modelo.Estados = Enum.GetValues<TareaEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    })
                    .ToList();
        }
    }
}