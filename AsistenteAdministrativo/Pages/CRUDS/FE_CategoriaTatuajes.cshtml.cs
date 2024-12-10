using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_CategoriaTatuajesModel : PageModel
    {
        private readonly CN_CategoriaTatuajes _cnCategoriaTatuajes;

        public FE_CategoriaTatuajesModel()
        {
          //  _cnCategoriaTatuajes = new CN_CategoriaTatuajes();
        }

        // Lista de categorías para mostrar en la página
        public List<E_CategoriaTatuajes> Categorias { get; set; } = new List<E_CategoriaTatuajes>();

        // Modelo para la categoría nueva o editada
        [BindProperty]
        public E_CategoriaTatuajes NuevaCategoria { get; set; }

        // Mensaje de salida para la operación
        public string Mensaje { get; set; }

        // Método para cargar las categorías al abrir la página
        public void OnGet()
        {
            string mensajeTemp;
           // Categorias = _cnCategoriaTatuajes.ListarCategorias(out mensajeTemp);
           // Mensaje = mensajeTemp;
        }

        // Método para registrar una nueva categoría
        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
               // bool exito = _cnCategoriaTatuajes.RegistrarCategoria(NuevaCategoria, out mensajeTemp);
                //TempData["Mensaje"] = mensajeTemp;
                //if (exito) return RedirectToPage();
            }
            return Page();
        }

        // Método para cargar datos de una categoría para edición
        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
           // NuevaCategoria = _cnCategoriaTatuajes.ListarCategorias(out mensajeTemp).Find(c => c.IdCategoriaTatuajes == id);
           // Mensaje = mensajeTemp;
            if (NuevaCategoria == null)
            {
                TempData["Mensaje"] = "Categoría no encontrada.";
                return RedirectToPage();
            }
            return Page();
        }

        // Método para eliminar una categoría
        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            //bool exito = _cnCategoriaTatuajes.EliminarCategoria(id, out mensajeTemp);
            //TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
