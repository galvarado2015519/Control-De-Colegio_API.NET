using ApiControlDeColegio.DTOs;
using ApiControlDeColegio.Entities;
using AutoMapper;

namespace ApiControlDeColegio.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Alumno, AlumnoDTO>();         
            CreateMap<AsignacionAlumno,AsignacionAlumnoDTO>();
            CreateMap<AsignacionAlumno,AsignacionAlumnoDetalleDTO>();
            CreateMap<AsignacionAlumnoDetalleDTO,AsignacionAlumno>();
            CreateMap<AsignacionAlumnoDTO, AsignacionAlumno>();
            CreateMap<Alumno,AlumnoAsignacionDTO>().ConstructUsing(a => new AlumnoAsignacionDTO{NombreCompleto = $"{a.Apellidos} {a.Nombres}"});
            CreateMap<Clase,ClaseAsignacionDTO>();   
            CreateMap<ClaseAsignacionDTO, Clase>();  
            CreateMap<Seminario, SeminarioDTO>();  
            CreateMap<SeminarioDTO, Seminario>();  
            CreateMap<Modulo, ModuloDTO>();  
            CreateMap<ModuloDTO, Modulo>();  
            CreateMap<Instructor, InstructorDTO>();  
            CreateMap<InstructorDTO, Instructor>();  
            CreateMap<DetalleNota, DetalleNotaDTO>();  
            CreateMap<DetalleActividad, DetalleActividadDTO>();  
        }
    }
}