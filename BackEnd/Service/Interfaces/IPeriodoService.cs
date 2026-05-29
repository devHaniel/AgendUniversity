using BackEnd.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Service.Interfaces
{
    public interface IPeriodoService
    {
        Task<List<PeriodoDto>> GetPeriodosAsync();
        Task<PeriodoDto> GetPeriodoByIdAsync(int id);
        Task<List<PeriodoDto>> GetPeriodosByUsuarioIdAsync(int usuarioId);
        Task<PeriodoDto> CreateAsync(PeriodoCreateDto dto);
        Task UpdateAsync(int id, PeriodoUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
