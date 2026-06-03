using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Models.Tarea
{
    public class TareaUpdateViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "La asignatura es obligatoria para evitar tareas sin asignar.")]
        public int AsignaturaId { get; set; }
        [Required(ErrorMessage = "El título es obligatorio para actualizar una tarea.")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de entrega")]
        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        public DateTime FechaEntrega { get; set; }
        public decimal Nota { get; set; }
        public decimal Calificacion { get; set; }
        public List<SelectListItem> Estados { get; set; }
        public List<SelectListItem> Asignaturas { get; set; } = new();
    }
}
