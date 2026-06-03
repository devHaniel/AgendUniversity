using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Models.Tarea
{
    public class TareaCreateViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "La asignatura es obligatoria para evitar tareas sin asignar.")]
        public int AsignaturaId { get; set; }
        [Required(ErrorMessage = "El título es obligatorio para crear una tarea.")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        [Display(Name = "Fecha de entrega")]
        [Required(ErrorMessage = "La fecha de entrega es obligatoria para evitar retrasos.")]
        public DateTime FechaEntrega { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd") == DateTime.UtcNow.ToString("yyyy-MM-dd") ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow;
        public decimal Nota { get; set; } = 0;
        public decimal Calificacion { get; set; }
        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> Asignaturas { get; set; } = new();
    }
}
