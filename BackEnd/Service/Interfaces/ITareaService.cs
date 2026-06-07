using BackEnd.Dtos;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
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
        Task<PagedResult<TareaDto>> GetTareasByUsuarioIdPagedAsync(int usuarioId, int page, int pageSize);
        Task<List<TareaDto>> GetTareasByAsignaturaIdAsync(int asignaturaId);
        Task<TareaDto> CreateAsync(TareaCreateDto dto);
        Task UpdateAsync(Guid id, TareaUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task<TareaArchivoDto> SubirArchivoAsync(Guid tareaId, IFormFile archivo);
        Task<TareaArchivoDownloadDto> GetArchivoParaDescargaAsync(int archivoId);
        Task<bool> DeleteArchivoAsync(int archivoId);
    }
}
