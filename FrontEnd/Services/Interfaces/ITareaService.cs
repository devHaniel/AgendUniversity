using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Models;
using FrontEnd.Models.Tarea;

namespace FrontEnd.Services.Interfaces
{
    public interface ITareaService
    {
        Task<List<FrontEnd.Models.Tarea.Tarea>> GetTareasByUsuario(int usuarioId);
        Task<PagedResult<FrontEnd.Models.Tarea.Tarea>> GetTareasByUsuarioPaged(int usuarioId, int page, int pageSize);
        Task<List<FrontEnd.Models.Tarea.Tarea>> GetTareasByAsignatura(int asignaturaId);
        Task<FrontEnd.Models.Tarea.Tarea> GetTareasById(Guid tareaId);
        Task<FrontEnd.Models.Tarea.Tarea> CreateTareaAsync(TareaCreateDto nuevaTarea);
        Task<bool> EditTareaAsync(Guid tareaId, TareaUpdateDto tareaActualizada);
        Task<bool> DeleteTareaAsync(Guid tareaId);
    }
}
