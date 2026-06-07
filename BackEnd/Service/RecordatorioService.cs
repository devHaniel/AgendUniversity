using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;
using BackEnd.Repository.Interfaces;
using BackEnd.Service.Interfaces;

namespace BackEnd.Service
{
    public class RecordatorioService : IRecordatorioService
    {
        private readonly IRecordatorioRepository repository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper mapper;

        public RecordatorioService(
            IRecordatorioRepository repository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }

        public async Task<List<RecordatorioDto>> GetRecordatoriosAsync()
        {
            var recordatorios = await repository.GetRecordatoriosAsync();
            return mapper.Map<List<RecordatorioDto>>(recordatorios);
        }

        public async Task<RecordatorioDto> GetRecordatorioByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var recordatorio = await repository.GetRecordatorioByIdAsync(id);
            return mapper.Map<RecordatorioDto>(recordatorio);
        }

        public async Task<List<RecordatorioDto>> GetRecordatoriosByUsuarioIdAsync(int usuarioId)
        {
            if (usuarioId <= 0)
                return new List<RecordatorioDto>();

            var recordatorios = await repository.GetRecordatoriosByUsuarioIdAsync(usuarioId);
            return mapper.Map<List<RecordatorioDto>>(recordatorios);
        }

        public async Task<RecordatorioDto> CreateAsync(RecordatorioCreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var usuario = await usuarioRepository.GetUsuarioByIdAsync(dto.UsuarioId);

            if (usuario is null)
                throw new ArgumentException("Usuario no encontrado.");

            var recordatorio = mapper.Map<Recordatorio>(dto);

            await repository.CreateAsync(recordatorio);
            await repository.SaveChangesAsync();

            return mapper.Map<RecordatorioDto>(recordatorio);
        }

        public async Task UpdateAsync(int id, RecordatorioUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Id de recordatorio inválido.");

            var recordatorio = await repository.GetRecordatorioByIdAsync(id);

            if (recordatorio is null)
                throw new ArgumentException("Recordatorio no encontrado.");

            mapper.Map(dto, recordatorio);

            repository.EditAsync(recordatorio);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recordatorio = await repository.GetRecordatorioByIdAsync(id);

            if (recordatorio is null)
                throw new ArgumentException("Recordatorio no encontrado.");

            repository.DeleteAsync(recordatorio);
            await repository.SaveChangesAsync();
        }
    }
}