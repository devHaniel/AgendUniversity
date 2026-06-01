using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Tarea
    {
        public Guid Id { get; set; }
        [Required]
        public int AsignaturaId { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Titulo { get; set; }
        [StringLength(maximumLength: 500)]
        public string Descripcion { get; set; }
        public int Estado { get; set; } = 1;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        [Required]
        public DateTime FechaEntrega { get; set; }
        public decimal Nota { get; set; }
        [Required]
        public decimal Calificacion { get; set; }
        public Asignatura Asignatura { get; set; }

    }
}
