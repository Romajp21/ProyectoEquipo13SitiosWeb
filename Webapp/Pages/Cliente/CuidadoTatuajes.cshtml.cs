using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class CuidadoTatuajesModel : PageModel
    {
        private readonly CN_CuidadosDelTatuaje _cnCuidados;

        public CuidadoTatuajesModel(IConfiguration configuration)
        {
            _cnCuidados = new CN_CuidadosDelTatuaje(configuration);
        }

        public List<E_CuidadosDelTatuaje> Cuidados { get; set; }

        public void OnGet()
        {
            // Obtener la lista de cuidados del tatuaje
            Cuidados = _cnCuidados.ObtenerCuidadosDelTatuaje();
        }
    }
}
