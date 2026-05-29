using BackEnd.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Service.Interfaces
{
    public interface ITareaService
    {
        Task<List<TareaDto>> GetTareasAsync();
        Task<TareaDto> GetTareaByIdAsync(Guid id);
        Task<List<TareaDto>> GetTareasByUsuarioIdAsync(int usuarioId);
        Task<List<TareaDto>> GetTareasByAsignaturaIdAsync(int asignaturaId);
        Task<TareaDto> CreateAsync(TareaCreateDto dto);
        Task UpdateAsync(Guid id, TareaUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
