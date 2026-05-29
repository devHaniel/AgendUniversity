using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Asignatura
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El ID del periodo es obligatorio.")]
        public int PeriodoId { get; set; }
        [Required(ErrorMessage = "El nombre de la asignatura es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la asignatura no puede exceder 100 caracteres.")]
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public List<Tarea> Tareas { get; set; } = new();
        public Periodo Periodo { get; set; }
    }
}