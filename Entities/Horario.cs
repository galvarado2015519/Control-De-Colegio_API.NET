using System;
using System.Collections.Generic;

namespace ApiControlDeColegio.Entities
{
    public class Horario
    {
        public string HorarioId {get; set;}
        public TimeSpan HorarioFinal {get; set;}
        public TimeSpan HorarioInicio {get;set;}
        public virtual List<Clase> Clases {get; set;}

        public Horario()
        {

        }

        public Horario(string HorarioId, TimeSpan HorarioFinal, TimeSpan HorarioInicial)
        {
            this.HorarioId = HorarioId;
            this.HorarioFinal = HorarioFinal;
            this.HorarioInicio = HorarioInicio;
        }
        
        public override string ToString()
        {
            return $"{this.HorarioInicio.ToString(@"hh\:mm")} - {this.HorarioFinal:hh\\:mm}";
        }
    }
}