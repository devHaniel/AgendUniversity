using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class AsignaturasIndexViewModel
    {
        public List<Periodo> Periodos { get; set; } = new();
        public List<Asignatura> Asignaturas { get; set; } = new();
        public int SelectedPeriodoId { get; set; }
    }
}
