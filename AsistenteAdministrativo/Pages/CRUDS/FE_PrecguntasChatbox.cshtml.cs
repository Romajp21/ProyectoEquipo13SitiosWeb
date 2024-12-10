using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_PreguntasChatboxModel : PageModel
    {
        private readonly CN_ChatboxPreguntaCrud _negocios = new CN_ChatboxPreguntaCrud();

        public List<E_ChatboxPreguntaCrud> Preguntas { get; set; } = new List<E_ChatboxPreguntaCrud>();
        [BindProperty]
        public E_ChatboxPreguntaCrud PreguntaActual { get; set; } = new E_ChatboxPreguntaCrud();



        public string Mensaje { get; set; }

        public void OnGet()
        {
            CargarPreguntas();
        }

        public void OnPostGuardarPregunta()
        {
            if (!ModelState.IsValid)
            {
                Mensaje = "Error: Verifica los campos del formulario.";
                CargarPreguntas();
                return;
            }

            bool resultado;
            if (PreguntaActual.IdPregunta == 0)
            {
                // Crear nueva pregunta
                resultado = _negocios.InsertarPregunta(PreguntaActual, out string mensaje);
                Mensaje = mensaje;
            }
            else
            {
                // Actualizar pregunta existente
                resultado = _negocios.ActualizarPregunta(PreguntaActual, out string mensaje);
                Mensaje = mensaje;
            }

            if (resultado)
            {
                PreguntaActual = new E_ChatboxPreguntaCrud(); // Limpia las casillas
                CargarPreguntas(); // Recarga las preguntas en la tabla
            }
            else
            {
                Mensaje = "Error: No se pudo guardar la pregunta.";
            }
        }


        public void OnPostEditarPregunta(int idPregunta)
        {
            PreguntaActual = _negocios.ObtenerPreguntaPorId(idPregunta, out string mensaje);

            if (PreguntaActual == null)
            {
                Mensaje = $"No se encontró la pregunta con el ID {idPregunta}.";
                PreguntaActual = new E_ChatboxPreguntaCrud();
            }
            else
            {
                Mensaje = "Pregunta obtenida correctamente.";
            }
        }

        public void OnPostEliminarPregunta(int idPregunta)
        {
            if (idPregunta <= 0)
            {
                Mensaje = "Error: ID de pregunta inválido.";
                return;
            }

            bool resultado = _negocios.EliminarPregunta(idPregunta, out string mensaje);
            Mensaje = mensaje;

            if (resultado)
            {
                CargarPreguntas();
            }
        }

        private void CargarPreguntas()
        {
            Preguntas = _negocios.ListarPreguntas(out string mensaje);
            Mensaje = mensaje;
        }
    }
}