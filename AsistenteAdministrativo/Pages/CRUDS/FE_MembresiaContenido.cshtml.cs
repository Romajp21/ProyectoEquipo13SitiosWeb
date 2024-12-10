using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_MembresiaContenidoModel : PageModel
    {
        private readonly CN_MembresiaContenido _cnMembresiaContenido;
        private readonly CN_Membresias _cnMembresias;
        private readonly CN_Notificaciones _cnNotificaciones;
        private readonly CN_Correo _cnCorreo;

        public FE_MembresiaContenidoModel(
            CN_MembresiaContenido cnMembresiaContenido,
            CN_Membresias cnMembresias,
            CN_Notificaciones cnNotificaciones,
            CN_Correo cnCorreo)
        {
            _cnMembresiaContenido = cnMembresiaContenido;
            _cnMembresias = cnMembresias;
            _cnNotificaciones = cnNotificaciones;
            _cnCorreo = cnCorreo;
            NuevoContenido = new E_MembresiaContenido();
            Contenidos = new List<E_MembresiaContenido>();
            Membresias = new List<E_Membresias>();
        }

        public List<E_MembresiaContenido> Contenidos { get; set; }
        public List<E_Membresias> Membresias { get; set; }

        [BindProperty]
        public E_MembresiaContenido NuevoContenido { get; set; }

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            CargarDatosIniciales();
        }

        public IActionResult OnPostGuardar()
        {
            ModelState.Clear(); // Limpiamos validaciones

            string mensaje;
            bool exito;

            if (NuevoContenido.IdMembresia == 0)
            {
                Mensaje = "Debe seleccionar una membresía.";
                CargarDatosIniciales();
                return Page();
            }

            if (NuevoContenido.IdMembresContenido == 0) // Crear
            {
                exito = _cnMembresiaContenido.RegistrarContenido(NuevoContenido, out mensaje);

                // Notificar al jefe de estudio
                if (exito)
                {
                    // Registrar la notificación
                    RegistrarNotificacion(
                        "Jefatura",
                        "Nueva Membresía",
                        "Nueva Membresía Creada",
                        $"Se ha creado un nuevo contenido: '{NuevoContenido.NombreContenido}' para la membresía '{NuevoContenido.IdMembresia}'."
                    );

                    // Enviar correos a la jefatura
                    var correos = _cnNotificaciones.ObtenerCorreos("Jefatura");
                    foreach (var correo in correos)
                    {
                        EnviarCorreo(
                            correo.Correo,
                            "Nueva Membresía Creada",
                            $"Se ha creado un nuevo contenido: '{NuevoContenido.NombreContenido}' para la membresía '{NuevoContenido.IdMembresia}'."
                        );
                    }
                }
            }
            else // Actualizar
            {
                exito = _cnMembresiaContenido.ActualizarContenido(NuevoContenido, out mensaje);
            }

            Mensaje = mensaje;

            if (exito)
            {
                return RedirectToPage();
            }

            CargarDatosIniciales();
            return Page();
        }

        public IActionResult OnGetCargarParaActualizar(int id)
        {
            string mensaje;
            var contenido = _cnMembresiaContenido.ListarContenido(null, out mensaje)
                              .Find(c => c.IdMembresContenido == id);

            if (contenido == null)
            {
                return new JsonResult(new { error = "Contenido no encontrado" });
            }

            return new JsonResult(new
            {
                idMembresContenido = contenido.IdMembresContenido,
                idMembresia = contenido.IdMembresia,
                nombreMembresia = contenido.NombreMembresia,
                nombreContenido = contenido.NombreContenido,
                comentario = contenido.Comentario,
                creadoPor = contenido.CreadoPor,
                estado = contenido.Estado
            });
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensaje;
            bool exito = _cnMembresiaContenido.EliminarContenido(id, out mensaje);
            Mensaje = mensaje;

            if (exito)
            {
                return RedirectToPage();
            }

            CargarDatosIniciales();
            return Page();
        }

        private void CargarDatosIniciales()
        {
            string mensaje;
            Contenidos = _cnMembresiaContenido.ListarContenido(null, out mensaje);
            Membresias = _cnMembresias.ListarMembresias(out mensaje);
        }

        private void RegistrarNotificacion(string modulo, string tipoEvento, string asunto, string mensaje)
        {
            // Registrar la notificación en la base de datos
            var notificacion = new E_Notificacion
            {
                Modulo = modulo,
                TipoEvento = tipoEvento,
                Asunto = asunto,
                Mensaje = mensaje,
            };

            string mensajeSalida;
            if (!_cnNotificaciones.RegistrarNotificacion(notificacion, out mensajeSalida))
            {
                Mensaje = $"Error al registrar la notificación: {mensajeSalida}";
            }
            else
            {
                Mensaje = "Notificación registrada exitosamente.";
            }
        }

        private void EnviarCorreo(string correoDestino, string asunto, string mensaje)
        {
            if (!_cnCorreo.EnviarCorreo(correoDestino, asunto, mensaje, out string error))
            {
                Mensaje = $"Error al enviar correo a {correoDestino}: {error}";
            }
        }
    }
}
