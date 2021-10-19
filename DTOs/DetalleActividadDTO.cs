using System;

namespace ApiControlDeColegio.DTOs
{
    public class DetalleActividadDTO
    {
        public string DetalleActividadId { get; set; }
        public string SeminarioId { get; set; }
        public string NombreActividad { get; set; }
        public char Estado { get; set; }
        public int NotaActividad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPostergacion { get; set; }
    }
}