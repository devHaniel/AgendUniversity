using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class PeriodoCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int Estado { get; set; } = 1;

        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; }
    }
}
