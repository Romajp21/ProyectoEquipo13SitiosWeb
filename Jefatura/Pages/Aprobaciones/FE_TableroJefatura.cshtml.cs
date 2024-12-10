using CapaNegocios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Jefatura.Pages.Aprobaciones
{
    public class FE_TableroJefaturaModel : PageModel
    {
        private readonly CN_Tablero _tableroNegocios;

        public FE_TableroJefaturaModel(CN_Tablero tableroNegocios)
        {
            _tableroNegocios = tableroNegocios;
        }

        public Dictionary<string, object> DatosTablero { get; set; } = new Dictionary<string, object>();

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            try
            {
                // Cargar los datos del tablero para el jefe de estudio
                DatosTablero = _tableroNegocios.ObtenerDatosTablero("Jefe", null, out string mensaje);

                if (!string.IsNullOrEmpty(mensaje))
                {
                    Mensaje = mensaje;
                }
            }
            catch (System.Exception ex)
            {
                // Captura cualquier error y muestra el mensaje
                Mensaje = $"Error al cargar el tablero: {ex.Message}";
            }
        }
    }
}
