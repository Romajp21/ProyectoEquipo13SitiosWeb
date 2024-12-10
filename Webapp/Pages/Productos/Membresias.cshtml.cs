using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.Productos
{
    public class Membresias : PageModel
    {
        private CN_Membresias negocio = new CN_Membresias();
        public List<E_Membresias> ListaMembresias { get; set; }
        public string Mensaje { get; set; }

        public void OnGet()
        {
            ListaMembresias = negocio.ListarMembresias(out string mensaje);
            Mensaje = mensaje;
        }

        public IActionResult Adquirir(int id)
        {
            // Asegúrate de que la lista esté cargada
            ListaMembresias = negocio.ListarMembresias(out string mensaje);
            Mensaje = mensaje;

            // Encuentra la membresía seleccionada
            var membresiaSeleccionada = ListaMembresias.FirstOrDefault(m => m.IdMembresia == id);

            if (membresiaSeleccionada != null)
            {
                // Redirige a la página de facturación con detalles de la membresía
                return RedirectToPage("/Productos/Facturacion", new { id = id });
            }
            else
            {
                Mensaje = "La membresía seleccionada no está disponible.";
                return Page();
            }
        }
    }
}
