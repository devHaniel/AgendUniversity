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
    public class TareaRepository : ITareaRepository
    {
        private readonly AppDbContext context;

        public TareaRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Tarea>> GetTareasAsync()
        {
            return await context.Tareas
                .Include(t => t.Asignatura)
                .ThenInclude(a => a.Periodo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Tarea> GetTareaByIdAsync(Guid id)
        {
            return await context.Tareas
                .Include(t => t.Asignatura)
                .ThenInclude(a => a.Periodo)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tarea>> GetTareasByUsuarioIdAsync(int usuarioId)
        {
            return await context.Tareas
                .Include(t => t.Asignatura)
                .ThenInclude(a => a.Periodo)
                .Where(t => t.Asignatura.Periodo.UsuarioId == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Tarea>> GetTareasByAsignaturaIdAsync(int asignaturaId)
        {
            return await context.Tareas
                .Include(t => t.Asignatura)
                .ThenInclude(a => a.Periodo)
                .Where(t => t.AsignaturaId == asignaturaId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateAsync(Tarea tarea)
        {
            await context.Tareas.AddAsync(tarea);
        }

        public void EditAsync(Tarea tarea)
        {
            context.Tareas.Update(tarea);
        }

        public void DeleteAsync(Tarea tarea)
        {
            context.Tareas.Remove(tarea);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
