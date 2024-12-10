using System.Collections.Generic;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class CN_Notificaciones
    {
        private CD_Notificaciones _datos = new CD_Notificaciones();

        public List<E_Notificacion> ObtenerNotificacionesPendientes()
        {
            return _datos.ObtenerNotificacionesPendientes();
        }

        public bool RegistrarNotificacion(E_Notificacion notificacion, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(notificacion.Correo) || string.IsNullOrWhiteSpace(notificacion.Asunto))
            {
                mensaje = "El correo y el asunto son obligatorios.";
                return false;
            }

            return _datos.RegistrarNotificacion(notificacion, out mensaje);
        }

        public bool ActualizarEstadoNotificacion(int idNotificacion, out string mensaje)
        {
            if (idNotificacion <= 0)
            {
                mensaje = "El ID de la notificación no es válido.";
                return false;
            }

            return _datos.ActualizarEstadoNotificacion(idNotificacion, out mensaje);
        }

        public List<E_Correo> ObtenerCorreos(string modulo)
        {
            if (string.IsNullOrWhiteSpace(modulo))
                throw new System.ArgumentException("El módulo no puede estar vacío.");

            return _datos.ObtenerCorreos(modulo);
        }

    }
}
