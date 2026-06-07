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
    public class RecordatoriosController : Controller
    {
        private readonly ILogger<RecordatoriosController> _logger;
        private readonly IRecordatoriosService _recordatorioService;

        public RecordatoriosController(ILogger<RecordatoriosController> logger,
            IRecordatoriosService recordatorioService)
        {
            _logger = logger;
            _recordatorioService = recordatorioService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            var recordatorios = await _recordatorioService.GetRecordatoriosByUsuarioIdAsync(userId);

            var resultado = recordatorios.Where(r => r.FechaRecordatorio >= DateTime.Now)
                .OrderBy(r => r.FechaRecordatorio)
                .ToList();

            return View(resultado);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}