using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class DetalleNota
    {
        public string DetalleNotaId { get; set; }
        public string DetalleActividadId { get; set; }
        public string Carne { get; set; }
        public int ValorNota { get; set; }
        
        public virtual List<DetalleActividad> DetalleActividad {get; set;}
        public virtual List<Alumno> Alumno {get; set;}
        public DetalleNota(){}
        public DetalleNota(string detalleNotaId, string detalleActividadId, string carne, int valorNota)
        {
            this.DetalleNotaId = detalleNotaId;
            this.DetalleActividadId = detalleActividadId;
            this.Carne = carne;
            this.ValorNota = valorNota;

        }
    }
}