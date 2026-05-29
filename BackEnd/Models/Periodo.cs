using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Periodo
    {
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre del periodo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del periodo no puede exceder 100 caracteres.")]
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; }
        public List<Asignatura> Asignaturas { get; set; } = new();
        public Usuario Usuario { get; set; }
    }
}