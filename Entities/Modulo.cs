using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Modulo
    {
        
        public string ModuloId {get; set;}
        public string CarreraId {get; set;}
        public string NombreModulo {get; set;}
        public int NumeroSeminarios {get; set;}
        
        public virtual Carrera Carrera {get; set;}
        public virtual List<Seminario> Seminario {get; set;}
    }
}