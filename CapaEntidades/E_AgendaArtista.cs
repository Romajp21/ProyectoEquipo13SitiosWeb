using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_AgendaArtista
    {
        public int IdAgendaArtista { get; set; }
        public int? IdHorarioArtista { get; set; }
        public int IdArtista { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Estado { get; set; } // 'Bloqueado' o 'Disponible'

        public E_AgendaArtista()
        {
            Estado = "Disponible"; // Valor por defecto
        }
    }
}
