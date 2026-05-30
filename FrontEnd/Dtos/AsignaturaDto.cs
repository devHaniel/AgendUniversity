using System.Collections.Generic;

namespace BackEnd.Dtos
{
    public class AsignaturaDto
    {
        public int Id { get; set; }
        public int PeriodoId { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public List<TareaDto> Tareas { get; set; }
    }
}