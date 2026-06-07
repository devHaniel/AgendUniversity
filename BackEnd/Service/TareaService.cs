using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;
using BackEnd.Repository.Interfaces;
using BackEnd.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackEnd.Service
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository repository;
        private readonly ITareaArchivoRepository archivoRepository;
        private readonly IAsignaturaRepository asignaturaRepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public TareaService(
            ITareaRepository repository,
            ITareaArchivoRepository archivoRepository,
            IAsignaturaRepository asignaturaRepository,
            IMapper mapper,
            IWebHostEnvironment environment)
        {
            this.repository = repository;
            this.archivoRepository = archivoRepository;
            this.asignaturaRepository = asignaturaRepository;
            this.mapper = mapper;
            this.environment = environment;
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

        public async Task<TareaArchivoDto> SubirArchivoAsync(Guid tareaId, IFormFile archivo)
        {
            if (tareaId == Guid.Empty)
                throw new ArgumentException("Id de tarea inválido.");

            if (archivo is null || archivo.Length == 0)
                throw new ArgumentException("Archivo inválido.");

            var tarea = await repository.GetTareaByIdAsync(tareaId);
            if (tarea is null)
                throw new ArgumentException("Tarea no encontrada.");

            var nombreOriginal = Path.GetFileName(archivo.FileName);
            var extension = Path.GetExtension(nombreOriginal);
            var nombreGuardado = $"{Guid.NewGuid()}{extension}";
            var uploadsPath = Path.Combine(environment.ContentRootPath, "Storage", "Tareas", tareaId.ToString());
            var rutaArchivo = Path.Combine(uploadsPath, nombreGuardado);

            Directory.CreateDirectory(uploadsPath);

            await using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var entity = new TareaArchivo
            {
                TareaId = tareaId,
                NombreOriginal = nombreOriginal,
                NombreGuardado = nombreGuardado,
                RutaArchivo = rutaArchivo,
                ContentType = string.IsNullOrWhiteSpace(archivo.ContentType)
                    ? "application/octet-stream"
                    : archivo.ContentType,
                TamanoBytes = archivo.Length
            };

            await archivoRepository.CreateAsync(entity);
            await archivoRepository.SaveChangesAsync();

            return mapper.Map<TareaArchivoDto>(entity);
        }

        public async Task<TareaArchivoDownloadDto> GetArchivoParaDescargaAsync(int archivoId)
        {
            if (archivoId <= 0)
                return null;

            var archivo = await archivoRepository.GetByIdAsync(archivoId);
            if (archivo is null || !File.Exists(archivo.RutaArchivo))
                return null;

            return new TareaArchivoDownloadDto
            {
                TareaId = archivo.TareaId,
                RutaArchivo = archivo.RutaArchivo,
                ContentType = archivo.ContentType,
                NombreOriginal = archivo.NombreOriginal
            };
        }

        public async Task<bool> DeleteArchivoAsync(int archivoId)
        {
            if (archivoId <= 0)
                return false;

            var archivo = await archivoRepository.GetByIdAsync(archivoId);
            if (archivo is null)
                return false;

            try
            {
                // Eliminar archivo físico si existe
                if (File.Exists(archivo.RutaArchivo))
                {
                    File.Delete(archivo.RutaArchivo);
                }

                // Eliminar registro de base de datos
                await archivoRepository.DeleteAsync(archivo);
                await archivoRepository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
