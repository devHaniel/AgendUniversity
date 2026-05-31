using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class TareaViewModel
    {
        public Asignatura Asignatura { get; set; }
        public List<Tarea> Tareas { get; set; }
    }
}