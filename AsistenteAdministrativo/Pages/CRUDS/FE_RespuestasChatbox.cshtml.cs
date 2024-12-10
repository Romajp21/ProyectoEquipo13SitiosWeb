using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_RespuestasChatboxModel : PageModel
    {
        private readonly CN_ChatboxRespuestaCrud _respuestasNegocios = new CN_ChatboxRespuestaCrud();
        private readonly CN_ChatboxPreguntaCrud _preguntasNegocios = new CN_ChatboxPreguntaCrud();

        public List<E_ChatboxRespuestaCrud> Respuestas { get; set; } = new List<E_ChatboxRespuestaCrud>();
        public List<E_ChatboxPreguntaCrud> Preguntas { get; set; } = new List<E_ChatboxPreguntaCrud>();

        [BindProperty]
        public E_ChatboxRespuestaCrud RespuestaActual { get; set; } = new E_ChatboxRespuestaCrud();

        public string Mensaje { get; set; }

        public void OnGet()
        {
            CargarPreguntas();
            CargarRespuestas();
        }

        public void OnPostGuardarRespuesta()
        {
            // Excluir campos que no deben ser validados en el formulario
            ModelState.Remove("RespuestaActual.TextoPregunta");

            if (!ModelState.IsValid)
            {
                Mensaje = "Error: Verifica los campos del formulario.";
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        Mensaje += $" Campo: {state.Key}, Error: {state.Value.Errors[0].ErrorMessage}";
                    }
                }
                CargarPreguntas();
                CargarRespuestas();
                return;
            }

            bool resultado;


            if (RespuestaActual.IdRespuesta > 0)
            {
                // Intentar actualizar
                var respuestaExistente = _respuestasNegocios.ObtenerRespuestaPorId(RespuestaActual.IdRespuesta, out string mensaje);
                if (respuestaExistente == null)
                {
                    Mensaje = $"Error: No se encontró la respuesta con el ID {RespuestaActual.IdRespuesta}.";
                    CargarPreguntas();
                    CargarRespuestas();
                    return;
                }

                resultado = _respuestasNegocios.ActualizarRespuesta(RespuestaActual, out mensaje);
                Mensaje = mensaje;
            }
            else
            {
                // Crear nueva respuesta
                resultado = _respuestasNegocios.InsertarRespuesta(RespuestaActual, out string mensaje);
                Mensaje = mensaje;
            }

            if (resultado)
            {
                RespuestaActual = new E_ChatboxRespuestaCrud(); // Limpia el modelo
                CargarPreguntas();
                CargarRespuestas();
            }
        }
        public void OnPostEditarRespuesta(int idRespuesta)
        {
            RespuestaActual = _respuestasNegocios.ObtenerRespuestaPorId(idRespuesta, out string mensaje);

            if (RespuestaActual == null)
            {
                Mensaje = $"No se encontró la respuesta con el ID {idRespuesta}.";
                RespuestaActual = new E_ChatboxRespuestaCrud();
            }
            else
            {
                Mensaje = "Respuesta obtenida correctamente.";
            }

            CargarPreguntas();
            CargarRespuestas();
        }

        public void OnPostEliminarRespuesta(int idRespuesta)
        {
            if (idRespuesta <= 0)
            {
                Mensaje = "Error: ID de respuesta inválido.";
                return;
            }

            bool resultado = _respuestasNegocios.EliminarRespuesta(idRespuesta, out string mensaje);
            Mensaje = mensaje;

            if (resultado)
            {
                CargarRespuestas();
            }
        }

        private void CargarPreguntas()
        {
            Preguntas = _preguntasNegocios.ListarPreguntas(out string mensaje);
        }

        private void CargarRespuestas()
        {
            Respuestas = _respuestasNegocios.ListarRespuestas(out string mensaje);
        }
    }
}
