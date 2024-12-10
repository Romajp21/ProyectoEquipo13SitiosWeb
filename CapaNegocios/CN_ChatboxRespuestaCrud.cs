using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_ChatboxRespuestaCrud
    {
        private readonly CD_ChatboxRespuestaCrud _capaDatos = new CD_ChatboxRespuestaCrud();

        public List<E_ChatboxRespuestaCrud> ListarRespuestas(out string mensaje)
        {
            return _capaDatos.ListarRespuestas(out mensaje);
        }

        public List<E_ChatboxRespuestaCrud> ObtenerRespuestasPorPregunta(int idPregunta, out string mensaje)
        {
            if (idPregunta <= 0)
            {
                mensaje = "El ID de la pregunta no es válido.";
                return new List<E_ChatboxRespuestaCrud>();
            }

            return _capaDatos.ObtenerRespuestasPorPregunta(idPregunta, out mensaje);
        }

        public E_ChatboxRespuestaCrud ObtenerRespuestaPorId(int idRespuesta, out string mensaje)
        {
            if (idRespuesta <= 0)
            {
                mensaje = "El ID de la respuesta no es válido.";
                return null;
            }

            return _capaDatos.ObtenerRespuestaPorId(idRespuesta, out mensaje);
        }

        public bool InsertarRespuesta(E_ChatboxRespuestaCrud respuesta, out string mensaje)
        {
            if (respuesta == null || string.IsNullOrWhiteSpace(respuesta.TextoRespuesta))
            {
                mensaje = "El texto de la respuesta no puede estar vacío.";
                return false;
            }

            if (respuesta.IdPregunta <= 0)
            {
                mensaje = "Debe seleccionar una pregunta válida.";
                return false;
            }

            if (respuesta.Orden <= 0)
            {
                mensaje = "El orden debe ser un número positivo.";
                return false;
            }

            return _capaDatos.InsertarRespuesta(respuesta, out mensaje);
        }

        public bool ActualizarRespuesta(E_ChatboxRespuestaCrud respuesta, out string mensaje)
        {
            if (respuesta == null || string.IsNullOrWhiteSpace(respuesta.TextoRespuesta))
            {
                mensaje = "El texto de la respuesta no puede estar vacío.";
                return false;
            }

            if (respuesta.IdRespuesta <= 0)
            {
                mensaje = "El ID de la respuesta no es válido.";
                return false;
            }

            if (respuesta.IdPregunta <= 0)
            {
                mensaje = "Debe seleccionar una pregunta válida.";
                return false;
            }

            if (respuesta.Orden <= 0)
            {
                mensaje = "El orden debe ser un número positivo.";
                return false;
            }

            return _capaDatos.ActualizarRespuesta(respuesta, out mensaje);
        }

        public bool EliminarRespuesta(int idRespuesta, out string mensaje)
        {
            if (idRespuesta <= 0)
            {
                mensaje = "El ID de la respuesta no es válido.";
                return false;
            }

            return _capaDatos.EliminarRespuesta(idRespuesta, out mensaje);
        }
    }
}
