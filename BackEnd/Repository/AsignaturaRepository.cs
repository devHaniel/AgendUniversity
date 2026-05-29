using BackEnd.Models;
using BackEnd.Persistencia;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Repository
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private readonly AppDbContext context;

        public AsignaturaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Asignatura>> GetAsignaturasAsync()
        {
            return await context.Asignaturas
                .Include(a => a.Tareas)
                .Include(a => a.Periodo)
                .ToListAsync();
        }

        public async Task<Asignatura> GetAsignaturaByIdAsync(int id)
        {
            return await context.Asignaturas
                .Include(a => a.Tareas)
                .Include(a => a.Periodo)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Asignatura>> GetAsignaturasByPeriodoIdAsync(int periodoId)
        {
            return await context.Asignaturas
                .Where(a => a.PeriodoId == periodoId)
                .Include(a => a.Tareas)
                .Include(a => a.Periodo)
                .ToListAsync();
        }

        public async Task CreateAsync(Asignatura asignatura)
        {
            await context.Asignaturas.AddAsync(asignatura);
        }

        public void EditAsync(Asignatura asignatura)
        {
            context.Asignaturas.Update(asignatura);
        }

        public void DeleteAsync(Asignatura asignatura)
        {
            context.Asignaturas.Remove(asignatura);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
