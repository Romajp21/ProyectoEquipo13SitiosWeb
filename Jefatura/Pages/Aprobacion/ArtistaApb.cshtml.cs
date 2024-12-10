using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Jefatura.Pages.Aprobacion
{
    public class ArtistaApbModel : PageModel
    {
        private readonly CN_Artista _cnArtista;

        public ArtistaApbModel(IConfiguration configuration)
        {
            _cnArtista = new CN_Artista(configuration);
        }

        public List<E_Artista> ListaArtistas { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            try
            {
                // Cargar todos los artistas (activos e inactivos)
                ListaArtistas = _cnArtista.ObtenerTodosLosArtistas();
            }
            catch (System.Exception ex)
            {
                Mensaje = $"Error al cargar los artistas: {ex.Message}";
            }
        }

        public IActionResult OnPostCambiarEstado(int idArtista, bool nuevoEstado)
        {
            try
            {
                // Actualizar el estado del artista
                _cnArtista.ActualizarEstadoArtista(idArtista, nuevoEstado);

                Mensaje = "El estado del artista ha sido actualizado exitosamente.";
                return RedirectToPage("./ArtistaApb");
            }
            catch (System.Exception ex)
            {
                Mensaje = $"Error al actualizar el estado del artista: {ex.Message}";
                return Page();
            }
        }
    }
}
