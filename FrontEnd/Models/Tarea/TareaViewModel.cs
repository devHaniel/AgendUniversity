using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models.Tarea
{
    public class TareaViewModel
    {
        public Asignatura.Asignatura Asignatura { get; set; }
        public List<Tarea> Tareas { get; set; }
    }
}
