using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios; // Cambiar seg�n tu estructura
using CapaEntidades; // Cambiar seg�n tu estructura

namespace Webapp.Pages.Productos
{
    public class FacturacionModel : PageModel
    {
        public Membresia Membresia { get; set; } // Propiedad para la membres�a seleccionada

        public void OnGet(int id)
        {
            // Simula la carga de datos desde la base de datos (reemplaza con l�gica real)
            var membresiasDisponibles = new List<Membresia>
            {
                new Membresia { IdMembresia = 1, NombreMembresia = "B�sica", Precio = 10, Duracion = 30 },
                new Membresia { IdMembresia = 2, NombreMembresia = "Premium", Precio = 25, Duracion = 90 },
                new Membresia { IdMembresia = 3, NombreMembresia = "VIP", Precio = 50, Duracion = 365 }
            };

            Membresia = membresiasDisponibles.FirstOrDefault(m => m.IdMembresia == id);

            if (Membresia == null)
            {
                // Maneja el caso donde no se encuentra la membres�a
                Membresia = new Membresia
                {
                    IdMembresia = id,
                    NombreMembresia = "No encontrada",
                    Precio = 0,
                    Duracion = 0
                };
            }
        }

        public IActionResult OnPost()
        {
            // Aqu� procesar�as el pago o lo que se requiera
            return RedirectToPage("/Productos/Confirmacion"); // Cambia la redirecci�n seg�n tus necesidades
        }
    }

    // Clase de ejemplo para la membres�a (ajusta seg�n tu estructura real)
    public class Membresia
    {
        public int IdMembresia { get; set; }
        public string NombreMembresia { get; set; }
        public decimal Precio { get; set; }
        public int Duracion { get; set; }
    }
}
