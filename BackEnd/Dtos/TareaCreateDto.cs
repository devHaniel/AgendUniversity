using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class TareaCreateDto
    {
        [Required]
        public int AsignaturaId { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        public int Estado { get; set; } = 1;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaEntrega { get; set; }
        public decimal Calificacion { get; set; }
    }
}
