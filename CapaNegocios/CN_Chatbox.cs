using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Chatbox
    {
        private readonly CD_Chatbox objCapaDatos = new CD_Chatbox();

        // Obtener preguntas iniciales
        public List<E_Chatbox> ObtenerPreguntasIniciales(out string mensaje)
        {
            return objCapaDatos.ObtenerPreguntasIniciales(out mensaje);
        }

        // Obtener respuestas asociadas a una pregunta
        public List<E_ChatboxRespuesta> ObtenerRespuestasPorPregunta(int idPregunta, out string mensaje)
        {
            if (idPregunta <= 0)
            {
                mensaje = "El ID de la pregunta no es válido.";
                return null;
            }

            return objCapaDatos.ObtenerRespuestasPorPregunta(idPregunta, out mensaje);
        }

        // Obtener una pregunta por su ID
        public E_Chatbox ObtenerPreguntaPorId(int idPregunta, out string mensaje)
        {
            mensaje = string.Empty;

            if (idPregunta <= 0)
            {
                mensaje = "El ID de la pregunta no es válido.";
                return null;
            }

            // Llamar a la capa de datos para obtener la pregunta por ID
            return objCapaDatos.ObtenerPreguntaPorId(idPregunta, out mensaje);
        }
    }
}
