using System;
using BackEnd.Models;

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
        public decimal Nota { get; set; }
        public decimal Calificacion { get; set; }
        public Asignatura asignatura;
        public List<TareaArchivoDto> Archivos { get; set; } = new();
    }
}
