using System;

namespace ApiControlDeColegio.DTOs
{
    public class SeminarioDTO
    {
        public string SeminarioId {get; set;}
        public string ModuloId {get; set;}
        public string NombreSeminario {get; set;}
        public DateTime FechaInicio {get; set;}

    }
}