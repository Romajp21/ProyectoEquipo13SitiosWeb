using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_CategoriaProductosModel : PageModel
    {
        private readonly CN_CategoriaProductos _cnCategorias;

        public FE_CategoriaProductosModel()
        {
            _cnCategorias = new CN_CategoriaProductos();
        }

        public List<E_CategoriaProductos> Categorias { get; set; } = new List<E_CategoriaProductos>();

        [BindProperty]
        public E_CategoriaProductos NuevaCategoria { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Categorias = _cnCategorias.Listar(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnCategorias.Registrar(NuevaCategoria, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnCategorias.Eliminar(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
