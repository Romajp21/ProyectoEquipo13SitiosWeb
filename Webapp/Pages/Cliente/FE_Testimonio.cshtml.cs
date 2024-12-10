using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_TestimonioModel : PageModel
    {
        private readonly CN_Testimonio _testimonioNegocios = new CN_Testimonio();

        public List<E_Citas> CitasSinTestimonio { get; set; } = new List<E_Citas>();
        public string Mensaje { get; set; }
        public bool Resultado { get; set; }

        [BindProperty]
        public E_Testimonio NuevoTestimonio { get; set; } = new E_Testimonio();

        public void OnGet()
        {
            int idCliente = 1; // Reemplaza con el ID del cliente autenticado
            CitasSinTestimonio = _testimonioNegocios.ListarCitasSinTestimonios(idCliente, out var mensaje);
            Mensaje = mensaje;
        }

        public IActionResult OnPost()
        {
            if (NuevoTestimonio.IdCita <= 0 || string.IsNullOrEmpty(NuevoTestimonio.Testimonio) || NuevoTestimonio.Calificacion <= 0)
            {
                Mensaje = "Todos los campos son obligatorios.";
                Resultado = false;
                return Page();
            }

            int idCliente = 1; // Reemplaza con el ID del cliente autenticado
            NuevoTestimonio.IdCliente = idCliente;

            var resultado = _testimonioNegocios.RegistrarTestimonio(NuevoTestimonio, out var mensaje);
            Mensaje = mensaje;
            Resultado = resultado;

            if (resultado)
                return RedirectToPage(); // Refresca la página tras un envío exitoso

            return Page();
        }
    }
}
