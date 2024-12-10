using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Artistas.Pages.Perfil
{
    public class PerfilModel : PageModel
    {
        private readonly CN_Artista _cnArtista;

        // ID estático del artista para la demo
        private const int ArtistaIdEstatico = 1;

        public PerfilModel(IConfiguration configuration)
        {
            _cnArtista = new CN_Artista(configuration);
        }

        [BindProperty]
        public E_Artista Artista { get; set; }

        public IActionResult OnGet()
        {
            Artista = _cnArtista.ObtenerDetalleArtista1(ArtistaIdEstatico);

            if (Artista == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostActualizarBio()
        {
            if (Artista != null && !string.IsNullOrWhiteSpace(Artista.Bio))
            {
                _cnArtista.ActualizarBio(ArtistaIdEstatico, Artista.Bio);
                TempData["Mensaje"] = "Biografía actualizada correctamente.";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostActualizarImagen(IFormFile ArchivoImagen)
        {
            if (ArchivoImagen != null && ArchivoImagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    ArchivoImagen.CopyTo(ms);
                    var imagenBase64 = Convert.ToBase64String(ms.ToArray());

                    // Guardar imagen en la base de datos
                    _cnArtista.GuardarImagenPerfil(ArtistaIdEstatico, imagenBase64);
                    TempData["Mensaje"] = "Imagen de perfil actualizada correctamente.";
                }
            }

            return RedirectToPage();
        }
    }
}
