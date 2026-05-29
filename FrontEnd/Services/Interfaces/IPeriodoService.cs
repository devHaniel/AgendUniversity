using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos;
using FrontEnd.Models;

namespace FrontEnd.Services.Interfaces
{
    public interface IPeriodoService
    {
        Task<List<Periodo>> GetPeriodosByUsuarioIdAsync(int usuarioId);
        Task<Periodo> CreatePeriodoAsync(PeriodoCreateDto dto);
        Task<Periodo> GetPeriodoByIdAsync(int id);
        Task<bool> EditPeriodoAsync(int id, PeriodoUpdateDto dto);
        Task<bool> DeletePeriodoAsync(int id);
    }
}