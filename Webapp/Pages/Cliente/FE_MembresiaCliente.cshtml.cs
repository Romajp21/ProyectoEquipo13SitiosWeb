using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_MembresiaClienteModel : PageModel
    {
        private readonly CN_ClienteMembresia _clienteMembresia = new CN_ClienteMembresia();

        public List<Membresia> Membresias { get; set; } = new List<Membresia>();

        public void OnGet()
        {
            // Obtenemos las membresías desde la capa de negocio
            string mensaje;
            var datosMembresias = _clienteMembresia.ListarMembresias(out mensaje);

            if (!string.IsNullOrEmpty(mensaje))
            {
                // Puedes manejar el mensaje de error aquí si es necesario
            }

            // Convertimos las entidades a un modelo adecuado para la vista
            foreach (var membresia in datosMembresias)
            {
                Membresias.Add(new Membresia
                {
                    IdMembresia = membresia.IdMembresia,
                    Nombre = membresia.NombreMembresia,
                    Precio = membresia.Precio.ToString("C"),
                    //EsPremium = membresia.Precio > 100, // Ejemplo: considera premium si el precio es mayor a 100
                    Beneficios = new List<string>()
                });

                foreach (var contenido in membresia.Contenidos)
                {
                    Membresias[^1].Beneficios.Add(contenido.NombreContenido);
                }
            }
        }
    }

    public class Membresia
    {
        public int IdMembresia { get; set; } // Agregar esta propiedad
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public List<string> Beneficios { get; set; } = new List<string>();
    }

}
