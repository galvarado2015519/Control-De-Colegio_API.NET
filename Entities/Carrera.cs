using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Carrera
    {
        public string CarreraId {get; set;}
        public string Nombre {get; set;}
        public virtual List<Clase> Clases {get; set;}

        public Carrera()
        {
            
        }

        public Carrera(string CarreraId, string Nombre)
        {
            this.CarreraId = CarreraId;
            this.Nombre = Nombre;
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}