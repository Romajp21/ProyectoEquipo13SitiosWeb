using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webapp.Pages.Cliente
{
    public class FE_MembresiaActualModel : PageModel
    {
        private readonly CN_RegistroMembresias _membresias = new CN_RegistroMembresias();
        public E_RegistroMembresias Membresia { get; set; }
        public bool EsExpirada => Membresia?.FechaExpiracion < DateTime.Now;

        public void OnGet()
        {
            string mensaje;
            int idCliente = 1; // ID del cliente autenticado
            Membresia = _membresias.ObtenerMembresiaCliente(idCliente, out mensaje);

            if (Membresia == null)
            {
                TempData["Error"] = mensaje ?? "No tienes una membresía activa.";
            }
        }
    }
}
