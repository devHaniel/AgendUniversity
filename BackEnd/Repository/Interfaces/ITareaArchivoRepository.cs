using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repository.Interfaces
{
    public interface ITareaArchivoRepository
    {
        Task<TareaArchivo> GetByIdAsync(int id);
        Task<List<TareaArchivo>> GetByTareaIdAsync(Guid tareaId);
        Task CreateAsync(TareaArchivo archivo);
        Task DeleteAsync(TareaArchivo archivo);
        Task SaveChangesAsync();
    }
}
