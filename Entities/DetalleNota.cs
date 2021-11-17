using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class DetalleNota
    {
        public string DetalleNotaId { get; set; }
        public string DetalleActividadId { get; set; }
        public string Carne { get; set; }
        public int ValorNota { get; set; }
        
        public virtual DetalleActividad DetalleActividad {get; set;}
        public virtual Alumno Alumno {get; set;}
    }
}