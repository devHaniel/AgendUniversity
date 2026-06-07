using System;

namespace FrontEnd.Models.Tarea
{
    public class Tarea
    {
        public Guid Id { get; set; }
        public int AsignaturaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaEntrega { get; set; } = DateTime.Now;
        public decimal Nota { get; set; }
        public decimal Calificacion { get; set; }
        public Asignatura.Asignatura Asignatura { get; set; }
        public List<TareaArchivo> Archivos { get; set; } = new();
    }
}
