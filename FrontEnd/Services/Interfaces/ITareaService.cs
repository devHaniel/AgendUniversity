using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Models;

namespace FrontEnd.Services.Interfaces
{
    public interface ITareaService
    {
        Task<List<Tarea>> GetTareasByUsuario(int usuarioId);
        Task<List<Tarea>> GetTareasByAsignatura(int asignaturaId);
        Task<Tarea> GetTareasById(Guid tareaId);
        Task<Tarea> CreateTareaAsync(TareaDto nuevaTarea);
        Task<bool> EditTareaAsync(Guid tareaId, TareaDto tareaActualizada);
        Task<bool> DeleteTareaAsync(Guid tareaId);
    }
}