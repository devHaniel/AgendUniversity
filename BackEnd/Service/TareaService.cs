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
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository repository;
        private readonly IAsignaturaRepository asignaturaRepository;
        private readonly IMapper mapper;

        public TareaService(
            ITareaRepository repository,
            IAsignaturaRepository asignaturaRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.asignaturaRepository = asignaturaRepository;
            this.mapper = mapper;
        }

        public async Task<List<TareaDto>> GetTareasAsync()
        {
            var tareas = await repository.GetTareasAsync();
            return mapper.Map<List<TareaDto>>(tareas);
        }

        public async Task<TareaDto> GetTareaByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            var tarea = await repository.GetTareaByIdAsync(id);
            return mapper.Map<TareaDto>(tarea);
        }

        public async Task<List<TareaDto>> GetTareasByUsuarioIdAsync(int usuarioId)
        {
            if (usuarioId <= 0)
                return new List<TareaDto>();

            var tareas = await repository.GetTareasByUsuarioIdAsync(usuarioId);
            return mapper.Map<List<TareaDto>>(tareas);
        }

        public async Task<PagedResult<TareaDto>> GetTareasByUsuarioIdPagedAsync(int usuarioId, int page, int pageSize)
        {
            if (usuarioId <= 0)
            {
                return new PagedResult<TareaDto>
                {
                    Page = 1,
                    PageSize = 10
                };
            }

            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            if (pageSize > 100)
                pageSize = 100;

            var (items, totalItems) = await repository.GetTareasByUsuarioIdPagedAsync(usuarioId, page, pageSize);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
                (items, totalItems) = await repository.GetTareasByUsuarioIdPagedAsync(usuarioId, page, pageSize);
            }

            return new PagedResult<TareaDto>
            {
                Items = mapper.Map<List<TareaDto>>(items),
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<List<TareaDto>> GetTareasByAsignaturaIdAsync(int asignaturaId)
        {
            if (asignaturaId <= 0)
                return new List<TareaDto>();

            var tareas = await repository.GetTareasByAsignaturaIdAsync(asignaturaId);
            return mapper.Map<List<TareaDto>>(tareas);
        }

        public async Task<TareaDto> CreateAsync(TareaCreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var asignatura = await asignaturaRepository.GetAsignaturaByIdAsync(dto.AsignaturaId);
            if (asignatura is null)
                throw new ArgumentException("Asignatura no encontrada.");

            var tarea = mapper.Map<Tarea>(dto);
            tarea.Id = Guid.NewGuid();

            await repository.CreateAsync(tarea);
            await repository.SaveChangesAsync();

            return mapper.Map<TareaDto>(tarea);
        }

        public async Task UpdateAsync(Guid id, TareaUpdateDto dto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id de tarea inválido.");

            var tarea = await repository.GetTareaByIdAsync(id);
            if (tarea is null)
                throw new ArgumentException("Tarea no encontrada.");

            mapper.Map(dto, tarea);
            repository.EditAsync(tarea);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tarea = await repository.GetTareaByIdAsync(id);
            if (tarea is null)
                throw new ArgumentException("Tarea no encontrada.");

            repository.DeleteAsync(tarea);
            await repository.SaveChangesAsync();
        }
    }
}
