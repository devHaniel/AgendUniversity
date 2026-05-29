using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class Asignatura
    {
        public int Id { get; set; }
        public int PeriodoId { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public List<Tarea> Tareas { get; set; } = new();
    }
}
