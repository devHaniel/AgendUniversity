using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class RecordatorioUpdateDto
    {
        [Required]
        [StringLength(70)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaRecordatorio { get; set; }
    }
}