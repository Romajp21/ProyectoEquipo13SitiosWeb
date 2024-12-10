using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Webapp.Pages.Cliente
{
    public class FE_AdquirirMembresiaModel : PageModel
    {
        private readonly CN_ClienteMembresia _clienteMembresia = new CN_ClienteMembresia();
        private readonly CN_RegistroMembresias _registroMembresias = new CN_RegistroMembresias();

        public E_ClienteMembresia Membresia { get; set; }

        [BindProperty]
        public int IdMembresia { get; set; }

        [BindProperty]
        public decimal Monto { get; set; } // Campo para el monto

        [BindProperty]
        public string NumeroTarjeta { get; set; } // Campo para el número de tarjeta

        public IActionResult OnGet(int idMembresia)
        {
            string mensaje;

            // Obtener las membresías desde la capa de negocios
            var membresias = _clienteMembresia.ListarMembresias(out mensaje);
            Membresia = membresias?.FirstOrDefault(m => m.IdMembresia == idMembresia);

            // Si no encuentra la membresía, redirige a la lista de membresías
            if (Membresia == null)
            {
                TempData["Error"] = "No se pudo cargar la información de la membresía seleccionada.";
                return RedirectToPage("/Cliente/FE_MembresiaCliente");
            }

            // Precargar monto con el precio de la membresía
            Monto = Membresia.Precio;

            return Page();
        }

        public IActionResult OnPost(int idMembresia, decimal monto, string numeroTarjeta)
        {
            string mensaje;

            // Crear el registro para enviar al procedimiento almacenado
            var registro = new E_RegistroMembresias
            {
                IdCliente = 1, // Ejemplo: cliente autenticado
                IdMembresia = idMembresia
            };

            // Registrar la compra con los datos del formulario
            bool resultado = _registroMembresias.RegistrarCompraMembresia(registro, monto, numeroTarjeta, out mensaje);

            if (resultado)
            {
                TempData["Mensaje"] = "¡Membresía adquirida correctamente!";
                TempData.Remove("Error"); // Aseguramos que no se muestre ningún error
                return RedirectToPage("/Cliente/FE_MembresiaActual"); // Redirige a la página de membresía actual
            }
            else
            {
                TempData["Error"] = mensaje;
                TempData.Remove("Mensaje"); // Aseguramos que no se muestre ningún mensaje de éxito
                return Page();
            }
        }
    }
}





