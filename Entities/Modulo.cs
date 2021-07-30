using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Modulo
    {
        
        public string ModuloId {get; set;}
        public string CarreraId {get; set;}
        public string NombreModulo {get; set;}
        public int NumeroSeminarios {get; set;}
        
        public virtual List<Carrera> Carrera {get; set;}
        
        public Modulo(){}

        public Modulo(string moduloId, string carreraId, string nombreModulo, int numeroSeminarios) 
        {
            this.ModuloId = moduloId;
            this.CarreraId = carreraId;
            this.NombreModulo = nombreModulo;
            this.NumeroSeminarios = numeroSeminarios;       
        }       
    }
}