using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repository.Interfaces
{
    public interface IPeriodoRepository
    {
        Task<List<Periodo>> GetPeriodosAsync();
        Task<Periodo> GetPeriodoByIdAsync(int id);
        Task<List<Periodo>> GetPeriodosByUsuarioIdAsync(int usuarioId);
        Task CreateAsync(Periodo periodo);
        void EditAsync(Periodo periodo);
        void DeleteAsync(Periodo periodo);
        Task SaveChangesAsync();
    }
}
