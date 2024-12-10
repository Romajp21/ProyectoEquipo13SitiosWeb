using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_TableroAsistenteAdministrativoModel : PageModel
    {
        private readonly CN_Tablero _tableroNegocios;

        public Dictionary<string, object> DatosTablero { get; private set; } = new Dictionary<string, object>();

        [TempData]
        public string Mensaje { get; set; }

        public FE_TableroAsistenteAdministrativoModel(CN_Tablero tableroNegocios)
        {
            _tableroNegocios = tableroNegocios;
        }

        public void OnGet()
        {
            try
            {
                DatosTablero = _tableroNegocios.ObtenerDatosTablero("Asistente", null, out string mensaje);
                if (!string.IsNullOrEmpty(mensaje))
                {
                    Mensaje = mensaje;
                }
            }
            catch (System.Exception ex)
            {
                Mensaje = $"Error al cargar los datos del tablero: {ex.Message}";
            }
        }
    }
}
