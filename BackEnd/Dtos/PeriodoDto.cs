using System;
using System.Collections.Generic;

namespace BackEnd.Dtos
{
    public class PeriodoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<AsignaturaDto> Asignaturas { get; set; }
    }
}
