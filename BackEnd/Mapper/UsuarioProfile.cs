using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;

namespace BackEnd.Mapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<PeriodoCreateDto, Periodo>();
            CreateMap<PeriodoUpdateDto, Periodo>();
            CreateMap<Periodo, PeriodoDto>();
            CreateMap<AsignaturaCreateDto, Asignatura>();
            CreateMap<AsignaturaUpdateDto, Asignatura>();
            CreateMap<Asignatura, AsignaturaDto>();
            CreateMap<Tarea, TareaDto>();
            CreateMap<TareaCreateDto, Tarea>();
            CreateMap<TareaUpdateDto, Tarea>();
            CreateMap<TareaArchivo, TareaArchivoDto>();
            CreateMap<Recordatorio, RecordatorioDto>();

            CreateMap<RecordatorioCreateDto, Recordatorio>();

            CreateMap<RecordatorioUpdateDto, Recordatorio>();
        }
    }
}
