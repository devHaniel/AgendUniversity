using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class TareaArchivo
    {
        public int Id { get; set; }
        [Required]
        public Guid TareaId { get; set; }
        [Required]
        [StringLength(255)]
        public string NombreOriginal { get; set; }
        [Required]
        [StringLength(255)]
        public string NombreGuardado { get; set; }
        [Required]
        [StringLength(500)]
        public string RutaArchivo { get; set; }
        [Required]
        [StringLength(150)]
        public string ContentType { get; set; }
        public long TamanoBytes { get; set; }
        public DateTime FechaSubida { get; set; } = DateTime.Now;
        public Tarea Tarea { get; set; }
    }
}
