using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class PeriodoUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
