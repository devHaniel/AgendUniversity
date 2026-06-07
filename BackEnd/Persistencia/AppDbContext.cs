using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Persistencia
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaArchivo> TareaArchivos { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Recordatorio> Recordatorios { get; set; }
    }
}
