using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Webapp.Pages.Artista
{
    public class IndexArtistaModel : PageModel
    {
        private readonly CN_Artista cnArtista;

        public IndexArtistaModel(IConfiguration configuration)
        {
            // Inicializar la capa de negocios
            cnArtista = new CN_Artista(configuration);
        }

        public List<E_Artista> Artistas { get; set; }

        public void OnGet()
        {
            // Obtiene los artistas con sus imágenes desde la base de datos
            Artistas = cnArtista.ObtenerArtistas1();

            // Filtrar artistas para mostrar solo los destacados o activos (opcional)
            Artistas = Artistas.FindAll(a => a.Estado);
        }
    }
}
