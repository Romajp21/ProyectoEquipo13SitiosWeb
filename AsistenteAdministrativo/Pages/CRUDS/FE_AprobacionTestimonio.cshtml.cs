using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_AprobacionTestimonioModel : PageModel
    {
        private readonly CN_Testimonio _testimonioNegocios = new CN_Testimonio();

        public List<E_Testimonio> TestimoniosPendientes { get; set; } = new List<E_Testimonio>();
        public string Mensaje { get; set; }
        public bool Resultado { get; set; }

        public void OnGet()
        {
            // Limpiar el mensaje y resultado al cargar la página
            Mensaje = string.Empty;
            Resultado = false;

            // Obtener los testimonios pendientes de aprobación
            TestimoniosPendientes = _testimonioNegocios.ObtenerTestimoniosPendientes(out var mensaje);

            // Solo asignar mensaje si hay error
            if (!string.IsNullOrEmpty(mensaje))
            {
                Mensaje = mensaje;
            }
        }



        public IActionResult OnPostAprobar(int idTestimonio)
        {
            int idEmpleado = 1; // ID del empleado autenticado
            var resultado = _testimonioNegocios.AprobarRechazarTestimonio(idTestimonio, idEmpleado, true, out var mensaje);
            Resultado = resultado;

            // Mensaje se muestra solo si hay resultado relevante
            Mensaje = mensaje;

            // Recargar la lista
            OnGet();
            return Page();
        }

        public IActionResult OnPostRechazar(int idTestimonio)
        {
            int idEmpleado = 1; // ID del empleado autenticado
            var resultado = _testimonioNegocios.AprobarRechazarTestimonio(idTestimonio, idEmpleado, false, out var mensaje);
            Resultado = resultado;

            // Mensaje se muestra solo si hay resultado relevante
            Mensaje = mensaje;

            // Recargar la lista
            OnGet();
            return Page();
        }
    }
}
