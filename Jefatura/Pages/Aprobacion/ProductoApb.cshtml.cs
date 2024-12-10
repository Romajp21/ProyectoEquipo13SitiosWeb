using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Jefatura.Pages.Aprobacion
{
    public class ProductoApbModel : PageModel
    {
        private readonly CN_Productos _cnProductos;

        // Inicializamos la propiedad para evitar problemas con valores nulos
        public List<E_Producto> Productos { get; set; } = new List<E_Producto>();

        public ProductoApbModel(CN_Productos cnProductos)
        {
            _cnProductos = cnProductos ?? throw new ArgumentNullException(nameof(cnProductos));
        }

        public void OnGet()
        {
            try
            {
                Productos = _cnProductos.ObtenerProductosConDetalles();
            }
            catch (Exception ex)
            {
                // Manejo de errores para capturar fallos en la l�gica de negocio
                ModelState.AddModelError(string.Empty, $"Error al cargar los productos: {ex.Message}");
            }
        }

        public IActionResult OnPostCambiarEstado(int idProducto, bool nuevoEstado)
        {
            try
            {
                _cnProductos.CambiarEstadoProducto(idProducto, nuevoEstado);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Manejo de errores al cambiar el estado del producto
                ModelState.AddModelError(string.Empty, $"Error al cambiar el estado del producto: {ex.Message}");
                return Page();
            }
        }
    }
}
