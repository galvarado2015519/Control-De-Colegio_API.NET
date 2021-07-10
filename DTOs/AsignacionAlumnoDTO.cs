using System;
using System.ComponentModel.DataAnnotations;
// using ApiControlDeColegio.Helpers;

namespace ApiControlDeColegio.DTOs
{
    public class AsignacionAlumnoDTO{
        public string AsignacionId {get; set;}

        [Required(ErrorMessage = "El campo carné es requerido")]
        public string Carne {get; set;}

        [Required(ErrorMessage = "El campo Clase Id es requerido")]
        public string ClaseId {get; set;}
        
        [Required(ErrorMessage = "El campo de la fecha es requerido")]
        public DateTime FechaAsignacion {get; set;}
    }
}