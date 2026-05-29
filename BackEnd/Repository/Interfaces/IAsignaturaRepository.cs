using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repository.Interfaces
{
    public interface IAsignaturaRepository
    {
        Task<List<Asignatura>> GetAsignaturasAsync();
        Task<Asignatura> GetAsignaturaByIdAsync(int id);
        Task<List<Asignatura>> GetAsignaturasByPeriodoIdAsync(int periodoId);
        Task CreateAsync(Asignatura asignatura);
        void EditAsync(Asignatura asignatura);
        void DeleteAsync(Asignatura asignatura);
        Task SaveChangesAsync();
    }
}
