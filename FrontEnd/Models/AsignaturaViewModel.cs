using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class AsignaturaViewModel
    {
        public Periodo Periodo { get; set; }
        public List<Periodo> Periodos { get; set; } = null;
        public List<Asignatura> Asignaturas { get; set; } = new();
        public int TotalAsignaturas => Asignaturas.Count;
    }
}