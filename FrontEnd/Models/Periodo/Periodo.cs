using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FrontEnd.Models.Asignatura;

namespace FrontEnd.Models.Periodo
{
    public class Periodo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFin { get; set; }
        public List<Asignatura.Asignatura> Asignaturas { get; set; } = new();

        // Propiedad calculada para mostrar el estado del período como texto
        // en curso, finalizada
        public string EstadoPeriodo{
            get
            {
                var hoy = DateTime.Now;
                if(hoy < FechaInicio)
                    return "No iniciado";
                else if (hoy >= FechaInicio && hoy <= FechaFin)
                    return "En curso";
                else
                    return "Finalizado";
            }
        }
        
    }
}
