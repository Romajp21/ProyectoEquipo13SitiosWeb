using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Citas
    {
        private readonly CD_Citas objCapaDatos = new CD_Citas();

        // Listar todas las citas, filtradas por cliente o artista si se especifican
        public List<E_Citas> ListarCitas(int? idCliente, int? idArtista, out string mensaje)
        {
            return objCapaDatos.ListarCitas(idCliente, idArtista, out mensaje);
        }

        // Obtener horarios disponibles para el cliente
        public List<E_DisponibilidadCitas> ObtenerHorariosDisponibles(int? idArtista, int horasSesion, int sesionesEstimadas, out string mensaje)
        {
            return objCapaDatos.ObtenerHorariosDisponibles(idArtista, horasSesion, sesionesEstimadas, out mensaje);
        }

        // Registrar una nueva cita
        public bool RegistrarCita(E_Citas cita, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que todos los datos necesarios estén completos
            if (!ValidarCita(cita, out mensaje))
                return false;

            // Registrar la cita en la base de datos
            return objCapaDatos.RegistrarCita(cita, out mensaje);
        }

        // Actualizar una cita existente
        public bool ActualizarCita(E_Citas cita, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar los datos antes de actualizar
            if (!ValidarCita(cita, out mensaje))
                return false;

            return objCapaDatos.ActualizarCita(cita, out mensaje);
        }

        // Eliminar una cita
        public bool EliminarCita(int idCita, out string mensaje)
        {
            mensaje = string.Empty;

            if (idCita <= 0)
            {
                mensaje = "El ID de la cita no es válido.";
                return false;
            }

            return objCapaDatos.EliminarCita(idCita, out mensaje);
        }

        // Obtener lista de artistas disponibles
        public List<E_Artista> ObtenerArtistas(out string mensaje)
        {
            return objCapaDatos.ListarArtistas(out mensaje);
        }



        // Validar los datos de la cita
        private bool ValidarCita(E_Citas cita, out string mensaje)
        {
            mensaje = string.Empty;

            if (cita.IdCliente <= 0)
            {
                mensaje = "El ID del cliente no es válido.";
                return false;
            }

            if (cita.IdArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return false;
            }

            if (cita.Fecha == default)
            {
                mensaje = "La fecha de la cita no es válida.";
                return false;
            }

            if (cita.HoraInicio == default || cita.HoraFin == default)
            {
                mensaje = "Las horas de inicio y fin deben ser válidas.";
                return false;
            }

            if (cita.HoraFin <= cita.HoraInicio)
            {
                mensaje = "La hora de fin debe ser posterior a la hora de inicio.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(cita.Estado))
            {
                mensaje = "El estado de la cita no puede estar vacío.";
                return false;
            }

            // Validar el ID del horario del artista si es requerido
            if (cita.IdHorarioArtista == null || cita.IdHorarioArtista <= 0)
            {
                mensaje = "El ID del horario del artista no es válido.";
                return false;
            }

            return true;
        }
    }
}
