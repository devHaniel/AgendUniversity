using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Models;
using FrontEnd.Models.Asignatura;

namespace FrontEnd.Services.Interfaces
{
    public interface IAsignaturasService
    {
        Task<List<Asignatura>> GetAsignaturasByUsuarioIdAsync(int usuarioId);
        Task<Asignatura> GetAsignaturaByIdAsync(int id);
        Task<Asignatura> CreateAsignaturaAsync(AsignaturaCreateDto dto);
        Task<bool> DeleteAsignaturaAsync(int id);
        Task<bool> UpdateAsignaturaAsync(int id, AsignaturaUpdateDto dto);
    }
}