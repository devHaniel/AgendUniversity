using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models.Recordatorio
{
   public class Recordatorio
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaRecordatorio { get; set; }
    }
}