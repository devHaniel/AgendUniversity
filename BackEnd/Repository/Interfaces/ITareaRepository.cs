using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repository.Interfaces
{
    public interface ITareaRepository
    {
        Task<List<Tarea>> GetTareasAsync();
        Task<Tarea> GetTareaByIdAsync(Guid id);
        Task<List<Tarea>> GetTareasByUsuarioIdAsync(int usuarioId);
        Task<(List<Tarea> Items, int TotalItems)> GetTareasByUsuarioIdPagedAsync(int usuarioId, int page, int pageSize);
        Task<List<Tarea>> GetTareasByAsignaturaIdAsync(int asignaturaId);
        Task CreateAsync(Tarea tarea);
        void EditAsync(Tarea tarea);
        void DeleteAsync(Tarea tarea);
        Task SaveChangesAsync();
    }
}
