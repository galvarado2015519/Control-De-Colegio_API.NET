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
        public virtual List<Seminario> Seminario {get; set;}

        public DetalleActividad(){}
        public DetalleActividad(string detalleActividadId, string seminarioId, string nombreActividad, char estado, int notaActividad, DateTime fechaCreacion, DateTime fechaEntrega, DateTime fechaPostergacion)
        {
            this.DetalleActividadId = detalleActividadId;
            this.SeminarioId = seminarioId;
            this.NombreActividad = nombreActividad;
            this.Estado = estado;
            this.NotaActividad = notaActividad;
            this.FechaCreacion = fechaCreacion;
            this.FechaEntrega = fechaEntrega;
            this.FechaPostergacion = fechaPostergacion;

        }
    }
}