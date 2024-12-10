using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Artistas.Pages.CRUD
{
    public class FE_ArtistaCitasModel : PageModel
    {
        private readonly CN_ArtistaCitas _citasNegocios;

        public FE_ArtistaCitasModel(CN_ArtistaCitas citasNegocios)
        {
            _citasNegocios = citasNegocios;
        }

        // Lista de citas del artista
        public List<E_ArtistaCitas> CitasArtista { get; set; } = new List<E_ArtistaCitas>();

        // Mensaje para notificaciones
        [TempData]
        public string Mensaje { get; set; }

        // ID del Artista (se obtiene desde la sesión)
        [BindProperty(SupportsGet = true)]
        public int IdArtista { get; set; }

        // Cargar las citas del artista
        public void OnGet()
        {
            // Obtener el ID del artista desde la sesión
            if (!HttpContext.Session.Keys.Contains("IdArtista"))
            {
                // Si no está configurado, inicializa el ID como 1 para pruebas
                HttpContext.Session.SetInt32("IdArtista", 1);
            }

            IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            if (IdArtista == 0)
            {
                Mensaje = "No se encontró un ID de artista en la sesión.";
                Console.WriteLine("Error: ID de Artista no encontrado en sesión.");
                return;
            }

            // Obtener citas del artista
            string mensaje;
            CitasArtista = _citasNegocios.ListarCitasPorArtista(IdArtista, out mensaje);

            if (!string.IsNullOrEmpty(mensaje))
            {
                Mensaje = mensaje;
                Console.WriteLine($"Mensaje desde la capa de negocios: {mensaje}");
            }
        }

        public IActionResult OnPostCompletar(int id)
        {
            // Obtener el ID del artista desde la sesión
            IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            if (IdArtista == 0)
            {
                Mensaje = "No se encontró un ID de artista en la sesión.";
                return RedirectToPage();
            }

            string mensaje;
            if (_citasNegocios.ActualizarEstadoCita(id, out mensaje))
            {
                TempData["Mensaje"] = "Cita marcada como completada exitosamente.";
            }
            else
            {
                TempData["Mensaje"] = $"Error al completar la cita: {mensaje}";
            }

            return RedirectToPage(new { IdArtista });
        }




    }
}
