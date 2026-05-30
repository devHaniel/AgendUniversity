using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Models
{
    public class AsignaturaEditViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int PeriodoId { get; set; }

        public int Estado { get; set; }

        public List<SelectListItem> Periodos { get; set; } = new();

        public List<SelectListItem> Estados { get; set; } = new();
    }
}