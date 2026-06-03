using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEnd.Models.Tarea;

namespace FrontEnd.Models.Asignatura
{
    public class Asignatura
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PeriodoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public List<Tarea.Tarea> Tareas { get; set; } = new();

        // Representa la Nota total del estudiante en la asignatura, calculada a partir de las tareas.
        public decimal TotalNota => Tareas != null ? Tareas.Sum(t => t.Nota) : 0;

        // Representa el total de las califacaciones de las tareas
        public decimal TotalCalificaciones => Tareas != null ? Tareas.Sum(t => t.Calificacion) : 0;
    }
}
