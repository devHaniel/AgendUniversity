using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services.Interfaces
{
    public interface IAsignaturasService
    {
        Task<List<FrontEnd.Models.Asignatura>> GetAsignaturasByUsuarioIdAsync(int usuarioId);
        Task<FrontEnd.Models.Asignatura> GetAsignaturaByIdAsync(int id);
        Task<List<FrontEnd.Models.Asignatura>> GetAsignaturasByPeriodoIdAsync(int periodoId);
    }
}