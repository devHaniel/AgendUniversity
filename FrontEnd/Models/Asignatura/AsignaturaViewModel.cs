using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models.Periodo;

namespace FrontEnd.Models.Asignatura
{
    public class AsignaturaViewModel
    {
        public Periodo.Periodo Periodo { get; set; }
        public List<Periodo.Periodo> Periodos { get; set; } = null;
        public List<Asignatura> Asignaturas { get; set; } = new();
        public int TotalAsignaturas => Asignaturas.Count;
    }
}
