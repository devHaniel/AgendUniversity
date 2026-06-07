using System;

namespace BackEnd.Dtos
{
    public class TareaArchivoDownloadDto
    {
        public Guid TareaId { get; set; }
        public string RutaArchivo { get; set; }
        public string ContentType { get; set; }
        public string NombreOriginal { get; set; }
    }
}
