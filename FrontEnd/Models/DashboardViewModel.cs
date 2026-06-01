using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class DashboardViewModel
    {
        public Periodo Periodo { get; set; }
        public List<Asignatura> Asignaturas { get; set; } = new();
        public List<Tarea> Tareas {get; set;} = new();
    }
}