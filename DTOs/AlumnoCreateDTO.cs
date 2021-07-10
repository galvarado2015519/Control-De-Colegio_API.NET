using System.ComponentModel.DataAnnotations;

namespace ApiControlDeColegio.DTOs
{
    public class AlumnoCreateDTO
    {
        [Required]
        public string Apellidos {get;set;}
        [Required]
        public string Nombres {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
    }
}