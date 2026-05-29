using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;
using BackEnd.Repository.Interfaces;
using BackEnd.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Service
{
    public class PeriodoService : IPeriodoService
    {
        private readonly IPeriodoRepository repository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper mapper;

        public PeriodoService(
            IPeriodoRepository repository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }

        public async Task<List<PeriodoDto>> GetPeriodosAsync()
        {
            var periodos = await repository.GetPeriodosAsync();
            return mapper.Map<List<PeriodoDto>>(periodos);
        }

        public async Task<PeriodoDto> GetPeriodoByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var periodo = await repository.GetPeriodoByIdAsync(id);
            return mapper.Map<PeriodoDto>(periodo);
        }

        public async Task<List<PeriodoDto>> GetPeriodosByUsuarioIdAsync(int usuarioId)
        {
            if (usuarioId <= 0)
                return new List<PeriodoDto>();

            var periodos = await repository.GetPeriodosByUsuarioIdAsync(usuarioId);
            return mapper.Map<List<PeriodoDto>>(periodos);
        }

        public async Task<PeriodoDto> CreateAsync(PeriodoCreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var usuario = await usuarioRepository.GetUsuarioByIdAsync(dto.UsuarioId);
            if (usuario is null)
                throw new ArgumentException("Usuario no encontrado.");

            var periodo = mapper.Map<Periodo>(dto);
            await repository.CreateAsync(periodo);
            await repository.SaveChangesAsync();

            return mapper.Map<PeriodoDto>(periodo);
        }

        public async Task UpdateAsync(int id, PeriodoUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Id de periodo inválido.");

            var periodo = await repository.GetPeriodoByIdAsync(id);
            if (periodo is null)
                throw new ArgumentException("Periodo no encontrado.");

            mapper.Map(dto, periodo);
            repository.EditAsync(periodo);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var periodo = await repository.GetPeriodoByIdAsync(id);
            if (periodo is null)
                throw new ArgumentException("Periodo no encontrado.");

            repository.DeleteAsync(periodo);
            await repository.SaveChangesAsync();
        }
    }
}
