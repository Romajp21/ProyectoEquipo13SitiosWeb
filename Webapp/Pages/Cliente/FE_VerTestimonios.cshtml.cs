using CapaEntidades;
using CapaNegocios;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_VerTestimoniosModel : PageModel
    {
        private readonly CN_Testimonio _testimonioNegocios = new CN_Testimonio();

        public List<E_Testimonio> Testimonios { get; set; } = new List<E_Testimonio>();

        public string Mensaje { get; set; }

        public void OnGet()
        {
            // Obtener todos los testimonios aprobados
            Testimonios = _testimonioNegocios.ObtenerTestimoniosAprobados(int.MaxValue, out string mensaje);
            Mensaje = mensaje;
        }
    }
}
