using System;
using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class DetalleActividad
    {
        public string DetalleActividadId { get; set; }
        public string SeminarioId { get; set; }
        public string NombreActividad { get; set; }
        public char Estado { get; set; }
        public int NotaActividad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPostergacion { get; set; }
        public virtual Seminario Seminario {get; set;}
        public virtual List<DetalleNota> DetalleNota {get; set;}

    }
}