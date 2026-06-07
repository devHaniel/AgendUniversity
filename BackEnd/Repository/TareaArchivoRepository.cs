using BackEnd.Models;
using BackEnd.Persistencia;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class TareaArchivoRepository : ITareaArchivoRepository
    {
        private readonly AppDbContext context;

        public TareaArchivoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TareaArchivo> GetByIdAsync(int id)
        {
            return await context.TareaArchivos
                .Include(a => a.Tarea)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<TareaArchivo>> GetByTareaIdAsync(Guid tareaId)
        {
            return await context.TareaArchivos
                .Where(a => a.TareaId == tareaId)
                .OrderByDescending(a => a.FechaSubida)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateAsync(TareaArchivo archivo)
        {
            await context.TareaArchivos.AddAsync(archivo);
        }

        public async Task DeleteAsync(TareaArchivo archivo)
        {
            context.TareaArchivos.Remove(archivo);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
