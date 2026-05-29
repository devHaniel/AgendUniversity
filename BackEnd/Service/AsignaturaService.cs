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
    public class AsignaturaService : IAsignaturaService
    {
        private readonly IAsignaturaRepository repository;
        private readonly IPeriodoRepository periodoRepository;
        private readonly IMapper mapper;

        public AsignaturaService(
            IAsignaturaRepository repository,
            IPeriodoRepository periodoRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.periodoRepository = periodoRepository;
            this.mapper = mapper;
        }

        public async Task<List<AsignaturaDto>> GetAsignaturasAsync()
        {
            var asignaturas = await repository.GetAsignaturasAsync();
            return mapper.Map<List<AsignaturaDto>>(asignaturas);
        }

        public async Task<AsignaturaDto> GetAsignaturaByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var asignatura = await repository.GetAsignaturaByIdAsync(id);
            return mapper.Map<AsignaturaDto>(asignatura);
        }

        public async Task<List<AsignaturaDto>> GetAsignaturasByPeriodoIdAsync(int periodoId)
        {
            if (periodoId <= 0)
                return new List<AsignaturaDto>();

            var asignaturas = await repository.GetAsignaturasByPeriodoIdAsync(periodoId);
            return mapper.Map<List<AsignaturaDto>>(asignaturas);
        }

        public async Task<AsignaturaDto> CreateAsync(AsignaturaCreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var periodo = await periodoRepository.GetPeriodoByIdAsync(dto.PeriodoId);
            if (periodo is null)
                throw new ArgumentException("Periodo no encontrado.");

            var asignatura = mapper.Map<Asignatura>(dto);
            await repository.CreateAsync(asignatura);
            await repository.SaveChangesAsync();

            return mapper.Map<AsignaturaDto>(asignatura);
        }

        public async Task UpdateAsync(int id, AsignaturaUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Id de asignatura inválido.");

            var asignatura = await repository.GetAsignaturaByIdAsync(id);
            if (asignatura is null)
                throw new ArgumentException("Asignatura no encontrada.");

            mapper.Map(dto, asignatura);
            repository.EditAsync(asignatura);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var asignatura = await repository.GetAsignaturaByIdAsync(id);
            if (asignatura is null)
                throw new ArgumentException("Asignatura no encontrada.");

            repository.DeleteAsync(asignatura);
            await repository.SaveChangesAsync();
        }
    }
}
