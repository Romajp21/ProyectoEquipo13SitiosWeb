using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_ChatboxPreguntaCrud
    {
        private readonly CD_ChatboxPreguntaCrud _capaDatos = new CD_ChatboxPreguntaCrud();

        public List<E_ChatboxPreguntaCrud> ListarPreguntas(out string mensaje)
        {
            return _capaDatos.ListarPreguntas(out mensaje);
        }

        public bool InsertarPregunta(E_ChatboxPreguntaCrud pregunta, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(pregunta.TextoPregunta))
            {
                mensaje = "El texto de la pregunta no puede estar vacío.";
                return false;
            }

            return _capaDatos.InsertarPregunta(pregunta, out mensaje);
        }

        public bool ActualizarPregunta(E_ChatboxPreguntaCrud pregunta, out string mensaje)
        {
            if (pregunta == null || string.IsNullOrEmpty(pregunta.TextoPregunta))
            {
                mensaje = "El texto de la pregunta no puede estar vacío.";
                return false;
            }

            return _capaDatos.ActualizarPregunta(pregunta, out mensaje);
        }


        public bool EliminarPregunta(int idPregunta, out string mensaje)
        {
            if (idPregunta <= 0)
            {
                mensaje = "El ID de la pregunta no es válido.";
                return false;
            }

            return _capaDatos.EliminarPregunta(idPregunta, out mensaje);
        }

        // Obtener una pregunta por su ID
        public E_ChatboxPreguntaCrud ObtenerPreguntaPorId(int idPregunta, out string mensaje)
        {
            if (idPregunta <= 0)
            {
                mensaje = "El ID de la pregunta no es válido.";
                return null;
            }

            return _capaDatos.ObtenerPreguntaPorId(idPregunta, out mensaje);
        }


    }
}
