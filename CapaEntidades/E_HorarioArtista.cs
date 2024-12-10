using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_HorarioArtista
    {
        public int IdHorarioArtista { get; set; }
        public int IdArtista { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public bool Estado { get; set; }

        public E_HorarioArtista()
        {
            Estado = true; // Valor por defecto
        }
    }
}
