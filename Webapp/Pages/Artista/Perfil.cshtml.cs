using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Pages.Artista
{
    public class PerfilModel : PageModel
    {
        private readonly CN_Artista cnArtista;

        public PerfilModel(IConfiguration configuration)
        {
            cnArtista = new CN_Artista(configuration); // Pasar directamente IConfiguration
        }

        public E_Artista Artista { get; set; }
        public string ImagenPerfilRuta { get; set; } // Variable para la ruta de la imagen

        public IActionResult OnGet(int id)
        {
            Artista = cnArtista.ObtenerDetalleArtista1(id);
            ImagenPerfilRuta = cnArtista.ObtenerImagenPerfil(id); // Llama al método para obtener la ruta


            if (Artista == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
