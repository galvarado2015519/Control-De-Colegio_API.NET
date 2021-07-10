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
        
        public AsignacionAlumno()
        {
            
        }

        public AsignacionAlumno(string AsignacionId, string Carne, string ClaseId, DateTime FechaAsignacion)
        {
            this.AsignacionId = AsignacionId;
            this.Carne = Carne;
            this.ClaseId = ClaseId;
            this.FechaAsignacion = FechaAsignacion;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
        {
            string fechaAuxiliar = FechaAsignacion.ToShortDateString();
            if(!string.IsNullOrEmpty(fechaAuxiliar))
            {
                DateTime fechaSalida;
                if(DateTime.TryParse(fechaAuxiliar, out fechaSalida))
                {
                    yield return new ValidationResult("Fecha de asignación invalida", new string[]{nameof(FechaAsignacion)});
                }
            }
        }
    }
}