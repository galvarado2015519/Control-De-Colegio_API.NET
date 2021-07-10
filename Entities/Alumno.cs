using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Alumno
    {
        public string Carne {get; set;}
        public string NoExpediente {get; set;}
        public string Apellidos {get; set;}
        public string Nombres {get; set;}
        public string Email {get; set;}
        public virtual List<AsignacionAlumno> Asignaciones {get; set;}
        public Alumno(){

        }
        
        public Alumno(string Carne, string NoExpediente, string Apellidos, string Nombres, string Email)
        {
            this.Carne = Carne;
            this.NoExpediente = NoExpediente;
            this.Apellidos = Apellidos;
            this.Nombres = Nombres;
            this.Email = Email;
        }
    }
}