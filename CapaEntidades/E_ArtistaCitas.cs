namespace CapaEntidades
{
    public class E_ArtistaCitas
    {
        public int IdCita { get; set; } // ID de la cita
        public int IdCliente { get; set; } // ID del cliente
        public string NombreCliente { get; set; } // Nombre completo del cliente
        public int IdArtista { get; set; } // ID del artista
        public string NombreArtista { get; set; } // Nombre completo del artista
        public DateTime Fecha { get; set; } // Fecha de la cita
        public TimeSpan HoraInicio { get; set; } // Hora de inicio de la cita
        public TimeSpan HoraFin { get; set; } // Hora de fin de la cita
        public string Descripcion { get; set; } // Descripción del tatuaje o motivo de la cita
        public string Estado { get; set; } // Estado actual de la cita (Pendiente, Completada, etc.)
        public int? IdFormulario { get; set; } // ID del formulario de cotización asociado (si aplica)
                                               // Campo opcional para actualizar el estado directamente
        public string NuevoEstado { get; set; } // Nuevo estado para la cita (usado al actualizar)
    }
}
