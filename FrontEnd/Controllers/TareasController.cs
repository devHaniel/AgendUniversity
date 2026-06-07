using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Enums;
using FrontEnd.Models.Tarea;
using FrontEnd.Models.Asignatura;
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

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            _logger.LogInformation($"Obteniendo tareas para usuario ID: {userId}");
            var result = await _tareaService.GetTareasByUsuarioPaged(userId, page, pageSize);

            foreach (var tarea in result.Items)
            {
                tarea.Asignatura = await _asignaturaService
                    .GetAsignaturaByIdAsync(tarea.AsignaturaId);
            }

            return View(result);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var tarea = await _tareaService.GetTareasById(id);
            if (tarea == null)
                return NotFound();

            tarea.Asignatura = await _asignaturaService.GetAsignaturaByIdAsync(tarea.AsignaturaId);

            return View(tarea);
        }

        public async Task<IActionResult> Create(int asignaturaId = 0)
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

            if (asignaturaId > 0)
                modelo.AsignaturaId = asignaturaId;

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

                return RedirectToAction("Details", "Asignaturas", new { asignaturaId = model.AsignaturaId });

            }

            TempData["Mensaje"] = "No se creó la tarea.";

            await CargarListasTarea(model);
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var tarea = await _tareaService.GetTareasById(id);
            if (tarea == null)
                return NotFound();

            var modelo = new TareaUpdateViewModel
            {
                Id = tarea.Id,
                AsignaturaId = tarea.AsignaturaId,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Estado = tarea.Estado,
                FechaCreacion = tarea.FechaCreacion,
                FechaEntrega = tarea.FechaEntrega,
                Calificacion = tarea.Calificacion,
                Nota = tarea.Nota
            };
            await CargarListasTarea(modelo);
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, TareaUpdateDto model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new TareaUpdateViewModel
                {
                    Id = id,
                    AsignaturaId = model.AsignaturaId,
                    Titulo = model.Titulo,
                    Descripcion = model.Descripcion,
                    Estado = model.Estado,
                    FechaCreacion = model.FechaCreacion,
                    FechaEntrega = model.FechaEntrega,
                    Calificacion = model.Calificacion,
                    Nota = model.Nota
                };
                await CargarListasTarea(viewModel);
                return View(viewModel);
            }

            var success = await _tareaService.EditTareaAsync(id, model);

            if (success)
            {
                TempData["Success"] = "Tarea actualizada correctamente.";
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                // return RedirectToAction(nameof(Edit), new { id = id });
            }

            TempData["Error"] = "Error al actualizar la tarea.";
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Details", "Asignaturas", new { asignaturaId = model.AsignaturaId });


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, string returnUrl = null)
        {
            var tarea = await _tareaService.GetTareasById(id);

            if (tarea == null)
                return NotFound();

            var success = await _tareaService.DeleteTareaAsync(id);

            if (!success)
            {
                TempData["Error"] = "Error al eliminar la tarea.";
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Tarea eliminada correctamente.";
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubirArchivo(Guid id, IFormFile archivo)
        {
            var tarea = await _tareaService.GetTareasById(id);

            if (tarea == null)
                return NotFound();

            if (archivo == null || archivo.Length == 0)
            {
                TempData["Error"] = "Debe seleccionar un archivo.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var success = await _tareaService.SubirArchivoAsync(id, archivo);

            TempData[success ? "Success" : "Error"] = success
                ? "Archivo subido correctamente."
                : "Error al subir el archivo.";

            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> DescargarArchivo(int id)
        {
            var archivo = await _tareaService.DescargarArchivoAsync(id);

            if (archivo == null)
                return NotFound();

            return File(archivo.Contenido, archivo.ContentType, archivo.NombreOriginal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarArchivo(int id, Guid tareaId)
        {
            var success = await _tareaService.EliminarArchivoAsync(id);

            TempData[success ? "Success" : "Error"] = success
                ? "Archivo eliminado correctamente."
                : "Error al eliminar el archivo.";

            return RedirectToAction(nameof(Details), new { id = tareaId });
        }

        private async Task CargarListasTarea<T>(T modelo) where T : class
        {
            var userId = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

            var asignaturas = await _asignaturaService.GetAsignaturasByUsuarioIdAsync(userId);

            if (modelo is TareaCreateViewModel createModel)
            {
                createModel.Asignaturas = asignaturas
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nombre,
                        Selected = a.Id == createModel.AsignaturaId
                    })
                    .ToList();

                createModel.Estados = Enum.GetValues<TareaEnum>()
                        .Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString(),
                            Selected = (int)e == createModel.Estado
                        })
                        .ToList();
            }
            else if (modelo is TareaUpdateViewModel updateModel)
            {
                updateModel.Asignaturas = asignaturas
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nombre,
                        Selected = a.Id == updateModel.AsignaturaId
                    })
                    .ToList();

                updateModel.Estados = Enum.GetValues<TareaEnum>()
                        .Select(e => new SelectListItem
                        {
                            Value = ((int)e).ToString(),
                            Text = e.ToString(),
                            Selected = (int)e == updateModel.Estado
                        })
                        .ToList();
            }
        }


    }
}
