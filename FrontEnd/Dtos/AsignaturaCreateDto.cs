using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class AsignaturaCreateDto
    {
        [Required]
        public int PeriodoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int Estado { get; set; } = 1;
    }
}
