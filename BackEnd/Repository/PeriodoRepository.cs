using BackEnd.Models;
using BackEnd.Persistencia;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class PeriodoRepository : IPeriodoRepository
    {
        private readonly AppDbContext context;

        public PeriodoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Periodo>> GetPeriodosAsync()
        {
            return await context.Periodos
                .Include(p => p.Asignaturas)
                .ThenInclude(a => a.Tareas)
                .ToListAsync();
        }

        public async Task<Periodo> GetPeriodoByIdAsync(int id)
        {
            return await context.Periodos
                .Include(p => p.Asignaturas)
                .ThenInclude(a => a.Tareas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Periodo>> GetPeriodosByUsuarioIdAsync(int usuarioId)
        {
            return await context.Periodos
                .Where(p => p.UsuarioId == usuarioId)
                .Include(p => p.Asignaturas)
                .ThenInclude(a => a.Tareas)
                .ToListAsync();
        }

        public async Task CreateAsync(Periodo periodo)
        {
            await context.Periodos.AddAsync(periodo);
        }

        public void EditAsync(Periodo periodo)
        {
            context.Periodos.Update(periodo);
        }

        public void DeleteAsync(Periodo periodo)
        {
            context.Periodos.Remove(periodo);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
