using System;

namespace FrontEnd.Models.Tarea
{
    public class TareaArchivo
    {
        public int Id { get; set; }
        public Guid TareaId { get; set; }
        public string NombreOriginal { get; set; }
        public string ContentType { get; set; }
        public long TamanoBytes { get; set; }
        public DateTime FechaSubida { get; set; }
    }
}
