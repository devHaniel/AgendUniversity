using System;

namespace BackEnd.Dtos
{
    public class TareaArchivoDto
    {
        public int Id { get; set; }
        public Guid TareaId { get; set; }
        public string NombreOriginal { get; set; }
        public string ContentType { get; set; }
        public long TamanoBytes { get; set; }
        public DateTime FechaSubida { get; set; }
    }
}
