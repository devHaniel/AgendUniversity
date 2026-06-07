using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Models.Recordatorio;

namespace FrontEnd.Services.Interfaces
{
    public interface IRecordatoriosService
    {
        Task<List<Recordatorio>> GetRecordatoriosByUsuarioIdAsync(int usuarioId);

        Task<Recordatorio> GetRecordatorioByIdAsync(int id);

        Task<Recordatorio> CreateRecordatorioAsync(
            RecordatorioCreateDto dto);

        Task<bool> UpdateRecordatorioAsync(
            int id,
            RecordatorioUpdateDto dto);

        Task<bool> DeleteRecordatorioAsync(int id);
    }
}