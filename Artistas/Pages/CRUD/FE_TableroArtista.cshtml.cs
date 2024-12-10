using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Artistas.Pages.CRUD
{
    public class FE_TableroArtistaModel : PageModel
    {
        private readonly CN_Tablero _tableroNegocios;

        public FE_TableroArtistaModel(CN_Tablero tableroNegocios)
        {
            _tableroNegocios = tableroNegocios;
        }

        public Dictionary<string, object> DatosTablero { get; set; } = new Dictionary<string, object>();

        public void OnGet()
        {
            // Supongamos que el ID del artista se guarda en sesión
            int? idArtista = HttpContext.Session.GetInt32("IdArtista");
            string mensaje;

            DatosTablero = _tableroNegocios.ObtenerDatosTablero("Artista", idArtista, out mensaje);

            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewData["Error"] = mensaje;
            }
        }
    }
}
