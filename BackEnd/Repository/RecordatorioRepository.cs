using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Persistencia;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository
{
    public class RecordatorioRepository : IRecordatorioRepository
    {
        private readonly AppDbContext context;

        public RecordatorioRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Recordatorio>> GetRecordatoriosAsync()
        {
            return await context.Recordatorios
                .Include(r => r.Usuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Recordatorio?> GetRecordatorioByIdAsync(int id)
        {
            return await context.Recordatorios
                .Include(r => r.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Recordatorio>> GetRecordatoriosByUsuarioIdAsync(int usuarioId)
        {
            return await context.Recordatorios
                .Where(r => r.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateAsync(Recordatorio recordatorio)
        {
            await context.Recordatorios.AddAsync(recordatorio);
        }

        public void EditAsync(Recordatorio recordatorio)
        {
            context.Recordatorios.Update(recordatorio);
        }

        public void DeleteAsync(Recordatorio recordatorio)
        {
            context.Recordatorios.Remove(recordatorio);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}