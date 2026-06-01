using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class TareaUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaEntrega { get; set; }
        public decimal Nota { get; set; } = 0;
        public decimal Calificacion { get; set; }
    }
}
