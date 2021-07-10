using System;

namespace ApiControlDeColegio.DTOs
{
    public class AsignacionAlumnoDetalleDTO
    {
        public string AsignacionId {get; set;}
        public DateTime FechaAsignacion {get; set;}
        public AlumnoAsignacionDTO alumno;
        public ClaseAsignacionDTO clase;
    }
}