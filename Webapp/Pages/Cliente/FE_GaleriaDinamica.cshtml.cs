using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_GaleriaDinamicaModel : PageModel
    {
        private readonly CN_Galeria _galeriaNegocios = new CN_Galeria();

        // Lista para almacenar las imágenes de la galería
        public List<E_Galeria> ImagenesGaleria { get; set; } = new List<E_Galeria>();

        public string Mensaje { get; set; }

        public void OnGet()
        {
            // Crear una variable temporal para el mensaje
            string mensajeTemporal;

            // Obtener todas las imágenes sin filtro
            ImagenesGaleria = _galeriaNegocios.ListarGaleria(out mensajeTemporal);

            // Asignar el mensaje obtenido
            Mensaje = mensajeTemporal;
        }
    }
}
