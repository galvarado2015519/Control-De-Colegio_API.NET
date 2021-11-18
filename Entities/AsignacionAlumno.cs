using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApiControlDeColegio.Helpers;

namespace ApiControlDeColegio.Entities
{
    public class AsignacionAlumno
    {
        public string AsignacionId {get; set;}

        [Required(ErrorMessage = "El campo carné es requerido")]
        [Carne]
        public string Carne {get; set;}

        [Required(ErrorMessage = "El campo Clase Id es requerido")]
        public string ClaseId {get; set;}
        
        [Required(ErrorMessage = "El campo de la fecha es requerido")]
        public DateTime FechaAsignacion {get; set;}
        public virtual Alumno Alumno {get; set;}
        public virtual Clase Clase {get; set;}
    }
}