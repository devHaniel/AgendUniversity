using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;

namespace BackEnd.Service.Interfaces
{
    public interface IRecordatorioService
    {
        Task<List<RecordatorioDto>> GetRecordatoriosAsync();
        Task<RecordatorioDto> GetRecordatorioByIdAsync(int id);
        Task<List<RecordatorioDto>> GetRecordatoriosByUsuarioIdAsync(int usuarioId);

        Task<RecordatorioDto> CreateAsync(RecordatorioCreateDto dto);
        Task UpdateAsync(int id, RecordatorioUpdateDto dto);
        Task DeleteAsync(int id);
    }
}