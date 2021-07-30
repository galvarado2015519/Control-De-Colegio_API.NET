using System;
using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Seminario
    {
        
        public string SeminairoId {get; set;}
        public string ModuloId {get; set;}
        public string NombreSeminario {get; set;}
        public DateTime FechaInicio {get; set;}
        public DateTime FechaFin {get; set;}
        
        public virtual List<Modulo> Modulo {get; set;}

        public Seminario(){}

        public Seminario(string seminairoId, string moduloId, string nombreSeminario, DateTime fechaInicio, DateTime fechaFin) 
        {
            this.SeminairoId = seminairoId;
            this.ModuloId = moduloId;
            this.NombreSeminario = nombreSeminario;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
        }
    }
}