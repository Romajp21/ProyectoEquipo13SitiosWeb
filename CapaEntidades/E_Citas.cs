using System;

namespace CapaEntidades
{
    public class E_Citas
    {
        public int IdCita { get; set; }
        public int IdCliente { get; set; }
        public int? IdArtista { get; set; } // Puede ser opcional si no se selecciona un artista
        public int? IdHorarioArtista { get; set; } // Relación con el horario específico del artista

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Descripcion { get; set; }
        public int? IdFormulario { get; set; } // Relacionado con la cotización
        public string Estado { get; set; }
        public int? Token { get; set; } // Identificador único para relacionar sesiones con formularios

        public E_Citas()
        {
            Estado = "Pendiente"; // Valor por defecto
        }
    }
    public class E_DisponibilidadCitas
    {
        public int? IdArtista { get; set; }
        public int? IdHorarioArtista { get; set; } // Identificador del horario en la tabla HorarioArtista

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Estado { get; set; }

        public E_DisponibilidadCitas()
        {
            Estado = "Disponible"; // Por defecto, es disponible hasta que se indique lo contrario.
        }
    }
}
