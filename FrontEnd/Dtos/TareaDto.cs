using System;

namespace BackEnd.Dtos
{
    public class TareaDto
    {
        public Guid Id { get; set; }
        public int AsignaturaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaEntrega { get; set; }
        public decimal Calificacion { get; set; }
    }
}