using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_TableroClienteModel : PageModel
    {
        private readonly CN_Tablero _tableroNegocios;

        public FE_TableroClienteModel(CN_Tablero tableroNegocios)
        {
            _tableroNegocios = tableroNegocios;
        }

        public Dictionary<string, object> DatosTablero { get; set; } = new Dictionary<string, object>();

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            int idCliente = 1; // Aquí deberías obtener el ID del cliente autenticado
            string mensaje;

            // Obtener los datos del tablero para el cliente
            DatosTablero = _tableroNegocios.ObtenerDatosTablero("Cliente", idCliente, out mensaje);

            if (!string.IsNullOrEmpty(mensaje))
            {
                Mensaje = mensaje;
            }
        }
    }
}
