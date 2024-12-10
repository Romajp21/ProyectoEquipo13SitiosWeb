using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Jefatura.Pages.Aprobaciones
{
    public class FE_AprobacionMembresiaContenidoModel : PageModel
    {
        private readonly CN_AprobacionMembresiaContenido _cnAprobacion;
        private readonly CN_Notificaciones _cnNotificaciones;
        private readonly CN_Correo _cnCorreo;

        public FE_AprobacionMembresiaContenidoModel(
            CN_AprobacionMembresiaContenido cnAprobacion,
            CN_Notificaciones cnNotificaciones,
            CN_Correo cnCorreo)
        {
            _cnAprobacion = cnAprobacion;
            _cnNotificaciones = cnNotificaciones;
            _cnCorreo = cnCorreo;
            Aprobaciones = new List<E_AprobacionMembresiaContenido>();
            EstadoOpciones = new List<SelectListItem>();
        }

        public List<E_AprobacionMembresiaContenido> Aprobaciones { get; set; }
        public List<SelectListItem> EstadoOpciones { get; set; }

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            // Definición de las opciones para el estado
            EstadoOpciones = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Aprobar" },
                new SelectListItem { Value = "0", Text = "Rechazar" }
            };

            // Cargar las aprobaciones
            CargarAprobaciones();
        }

        // Método para guardar una nueva aprobación
        public IActionResult OnPostGuardarAprobacion(int idMembresContenido, string comentario, int estado, int aprobadoPor)
        {
            string mensaje;

            // Crear instancia de aprobación con los datos recibidos
            var aprobacion = new E_AprobacionMembresiaContenido
            {
                IdMembresContenido = idMembresContenido,
                Estado = estado == 1,  // Convierte 1 o 0 a booleano
                Comentario = comentario,
                AprobadoPor = aprobadoPor
            };

            // Llamar al método de negocio para registrar la aprobación
            bool exito = _cnAprobacion.RegistrarAprobacion(aprobacion, out mensaje);
            if (exito)
            {
                var destinatario = ObtenerCorreoAsociado("Artistas", idMembresContenido); // Obtener correo dinámicamente
                var asunto = estado == 1 ? "Contenido Aprobado" : "Contenido Rechazado";
                var mensajeCorreo = GenerarMensajeCorreo(aprobacion, estado == 1);
                EnviarCorreo(destinatario, asunto, mensajeCorreo);
            }

            Mensaje = exito ? "Guardado exitosamente." : mensaje;

            // Recargar la lista de aprobaciones después de guardar
            CargarAprobaciones();

            // Retornar a la página actual
            return Page();
        }


        // Método para actualizar una aprobación existente
        public IActionResult OnPostActualizarAprobacion(int id, int idMembresContenido, string comentario, int estado, int aprobadoPor)
        {
            string mensaje;

            if (id <= 0 || idMembresContenido <= 0 || aprobadoPor <= 0)
            {
                Mensaje = "Error: Todos los campos son obligatorios.";
                CargarAprobaciones();
                return Page();
            }

            var aprobacion = new E_AprobacionMembresiaContenido
            {
                IdAprobacion = id,
                IdMembresContenido = idMembresContenido,
                Estado = estado == 1,
                Comentario = comentario,
                AprobadoPor = aprobadoPor
            };

            bool exito = _cnAprobacion.ActualizarAprobacion(aprobacion, out mensaje);
            if (exito)
            {
                var destinatario = ObtenerCorreoAsociado("Artistas", idMembresContenido); // Obtener correo dinámicamente
                var asunto = estado == 1 ? "Contenido Aprobado" : "Contenido Rechazado";
                var mensajeCorreo = GenerarMensajeCorreo(aprobacion, estado == 1);
                EnviarCorreo(destinatario, asunto, mensajeCorreo);
            }

            Mensaje = exito ? "Aprobación actualizada exitosamente." : mensaje;

            CargarAprobaciones();
            return Page();
        }


        // Método para eliminar una aprobación
        public IActionResult OnPostEliminar(int id)
        {
            string mensaje;
            bool exito = _cnAprobacion.EliminarAprobacion(id, out mensaje);
            Mensaje = exito ? "Aprobación eliminada exitosamente." : mensaje;

            if (exito)
            {
                return RedirectToPage();
            }

            CargarAprobaciones();
            return Page();
        }



        // Método para cargar la lista de aprobaciones
        private void CargarAprobaciones()
        {
            string mensaje;
            Aprobaciones = _cnAprobacion.ListarAprobaciones(null, null, out mensaje);

            // No asignar ningún mensaje en este método
        }

        private string ObtenerCorreoAsociado(string modulo, int idMembresContenido)
        {
            var correos = _cnNotificaciones.ObtenerCorreos(modulo);
            var correoAsociado = correos.Find(c => c.Modulo == modulo)?.Correo;

            if (string.IsNullOrEmpty(correoAsociado))
            {
                Mensaje = $"Error: No se encontró un correo asociado al módulo {modulo} para el contenido {idMembresContenido}.";
                return string.Empty;
            }

            return correoAsociado;
        }
        private void EnviarCorreo(string correoDestino, string asunto, string mensaje)
        {
            if (!_cnCorreo.EnviarCorreo(correoDestino, asunto, mensaje, out string error))
            {
                Mensaje = $"Error al enviar correo a {correoDestino}: {error}";
            }
        }

        private string GenerarMensajeCorreo(E_AprobacionMembresiaContenido aprobacion, bool aprobado)
        {
            var estado = aprobado ? "aprobado" : "rechazado";
            return $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <div style='padding: 20px; border: 1px solid #ccc; border-radius: 8px;'>
                        <h2 style='color: #4CAF50;'>Estado del Contenido: {estado.ToUpper()}</h2>
                        <p>El contenido <strong>'{aprobacion.NombreContenido}'</strong> de la membresía <strong>'{aprobacion.NombreMembresia}'</strong> ha sido {estado}.</p>
                        <p>Comentario del jefe: {aprobacion.Comentario}</p>
                        <p><small>Este correo fue generado automáticamente. No respondas a este mensaje.</small></p>
                    </div>
                </body>
                </html>";
        }

    }
}
