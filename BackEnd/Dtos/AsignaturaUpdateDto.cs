using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos
{
    public class AsignaturaUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int Estado { get; set; }
    }
}
