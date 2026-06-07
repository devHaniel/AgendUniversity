using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class RecordatorioDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaRecordatorio { get; set; }
    }
}