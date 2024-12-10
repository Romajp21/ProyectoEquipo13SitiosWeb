using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_AgendaArtista
    {
        private readonly CD_AgendaArtista objCapaDatos = new CD_AgendaArtista();

        // Listar agenda con soporte para IdHorarioArtista
        public List<E_AgendaArtista> ListarAgenda(int? idArtista, int? idHorarioArtista, string fecha, out string mensaje)
        {
            return objCapaDatos.ListarAgenda(idArtista ?? 0, idHorarioArtista ?? 0, fecha, out mensaje);
        }

        // Registrar una nueva agenda
        public bool RegistrarAgenda(E_AgendaArtista agenda, out string mensaje)
        {
            if (agenda.IdAgendaArtista > 0)
            {
                mensaje = "La agenda ya existe. Use el flujo de edición.";
                return false;
            }

            return objCapaDatos.RegistrarAgenda(agenda, out mensaje);
        }

        // Actualizar una agenda existente
        public bool ActualizarAgenda(E_AgendaArtista agenda, out string mensaje)
        {
            if (agenda.IdAgendaArtista <= 0)
            {
                mensaje = "El ID de la agenda no es válido para actualizar.";
                return false;
            }

            return objCapaDatos.ActualizarAgenda(agenda, out mensaje);
        }

        // Eliminar una agenda por su ID
        public bool EliminarAgenda(int idAgenda, out string mensaje)
        {
            if (idAgenda <= 0)
            {
                mensaje = "El ID de la agenda no es válido.";
                return false;
            }

            return objCapaDatos.EliminarAgenda(idAgenda, out mensaje);
        }

        // Método para listar los horarios disponibles para un artista
        public List<E_HorarioArtista> ListarHorariosDisponibles(int idArtista, out string mensaje)
        {
            if (idArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return new List<E_HorarioArtista>();
            }

            return objCapaDatos.ListarHorariosDisponibles(idArtista, out mensaje);
        }

        // Validar los datos de la agenda
        private bool ValidarAgenda(E_AgendaArtista agenda, out string mensaje)
        {
            mensaje = string.Empty;

            if (agenda.IdArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return false;
            }

            if (agenda.Fecha == default)
            {
                mensaje = "La fecha no es válida.";
                return false;
            }

            if (agenda.HoraInicio >= agenda.HoraFin)
            {
                mensaje = "La hora de inicio debe ser menor que la hora de fin.";
                return false;
            }

            if (string.IsNullOrEmpty(agenda.Estado))
            {
                mensaje = "El estado de la agenda no puede estar vacío.";
                return false;
            }

            // Validar que IdHorarioArtista, si existe, sea válido
            if (agenda.IdHorarioArtista.HasValue && agenda.IdHorarioArtista <= 0)
            {
                mensaje = "El ID del horario no es válido.";
                return false;
            }
            return true;
        }

        public E_AgendaArtista ObtenerAgendaPorId(int idAgenda, out string mensaje)
{
    CD_AgendaArtista dataLayer = new CD_AgendaArtista();
    return dataLayer.ObtenerAgendaPorId(idAgenda, out mensaje);
}

    }
}
