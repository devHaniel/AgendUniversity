using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class DashboardViewModel
    {
        public Periodo.Periodo Periodo { get; set; }
        public List<Asignatura.Asignatura> Asignaturas { get; set; } = new();
        public List<Tarea.Tarea> Tareas {get; set;} = new();
    }
}