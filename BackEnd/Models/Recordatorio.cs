using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Recordatorio
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(70, ErrorMessage = "El título no puede exceder los 70 caracteres.")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaRecordatorio { get; set; }

        public Usuario Usuario { get; set; }
    }
}