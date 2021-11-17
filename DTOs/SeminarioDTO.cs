using System;

namespace ApiControlDeColegio.DTOs
{
    public class SeminarioDTO
    {
        public DateTime FechaInicio {get; set;}
        public DateTime FechaFin {get; set;}
        public string ModuloId {get; set;}
        public string NombreSeminario {get; set;}
        public string SeminarioId {get; set;}
        // public ModuloDTO modulo {get; set;}

    }
}