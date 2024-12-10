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
            // Aseg�rate de que la lista est� cargada
            ListaMembresias = negocio.ListarMembresias(out string mensaje);
            Mensaje = mensaje;

            // Encuentra la membres�a seleccionada
            var membresiaSeleccionada = ListaMembresias.FirstOrDefault(m => m.IdMembresia == id);

            if (membresiaSeleccionada != null)
            {
                // Redirige a la p�gina de facturaci�n con detalles de la membres�a
                return RedirectToPage("/Productos/Facturacion", new { id = id });
            }
            else
            {
                Mensaje = "La membres�a seleccionada no est� disponible.";
                return Page();
            }
        }
    }
}
