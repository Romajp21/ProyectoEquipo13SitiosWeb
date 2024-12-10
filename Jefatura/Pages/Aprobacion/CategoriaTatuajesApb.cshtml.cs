using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Jefatura.Pages.Aprobacion
{
    public class CategoriaTatuajesApbModel : PageModel
    {
        private readonly CN_CategoriaTatuajes _cnCategoria;

        public CategoriaTatuajesApbModel(IConfiguration configuration)
        {
            _cnCategoria = new CN_CategoriaTatuajes(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<E_CategoriaTatuajes> Categorias { get; set; }

        [BindProperty]
        public E_CategoriaTatuajes CategoriaEditar { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? IdCategoriaEliminar { get; set; }

        public void OnGet()
        {
            // Cargar las categorías
            Categorias = _cnCategoria.ObtenerCategorias();
        }

        public IActionResult OnPostEditarJefatura()
        {
            if (CategoriaEditar != null && CategoriaEditar.IdCategoriaTatuajes > 0)
            {
                try
                {
                    // Llamar al método específico de jefatura
                    _cnCategoria.EditarCategoriaJefatura(CategoriaEditar);
                    TempData["Mensaje"] = "Categoría actualizada correctamente por Jefatura.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al actualizar la categoría: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Datos de categoría inválidos.";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            if (IdCategoriaEliminar.HasValue)
            {
                try
                {
                    // Llamar a la capa de negocios para eliminar la categoría
                    _cnCategoria.EliminarCategoria(IdCategoriaEliminar.Value);
                    TempData["Mensaje"] = "Categoría eliminada correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al eliminar la categoría: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "No se pudo identificar la categoría a eliminar.";
            }

            return RedirectToPage();
        }
    }
}
