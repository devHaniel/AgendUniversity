using BackEnd.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Service.Interfaces
{
    public interface IAsignaturaService
    {
        Task<List<AsignaturaDto>> GetAsignaturasAsync();
        Task<AsignaturaDto> GetAsignaturaByIdAsync(int id);
        Task<List<AsignaturaDto>> GetAsignaturasByPeriodoIdAsync(int periodoId);
        Task<AsignaturaDto> CreateAsync(AsignaturaCreateDto dto);
        Task UpdateAsync(int id, AsignaturaUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
