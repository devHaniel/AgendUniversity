using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;

namespace BackEnd.Repository.Interfaces
{
    public interface IRecordatorioRepository
    {
        Task<List<Recordatorio>> GetRecordatoriosAsync();
        Task<Recordatorio> GetRecordatorioByIdAsync(int id);
        Task<List<Recordatorio>> GetRecordatoriosByUsuarioIdAsync(int usuarioId);
        Task CreateAsync(Recordatorio recordatorio);
        void EditAsync(Recordatorio recordatorio);
        void DeleteAsync(Recordatorio recordatorio);
        Task SaveChangesAsync();
    }
}