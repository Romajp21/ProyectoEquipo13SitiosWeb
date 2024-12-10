using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_CategoriaPreguntasFrecuentesModel : PageModel
    {
        private readonly CN_CategoriaPreguntasFrecuentes _cnCategoria;

        public FE_CategoriaPreguntasFrecuentesModel()
        {
            _cnCategoria = new CN_CategoriaPreguntasFrecuentes();
        }

        public List<E_CategoriaPreguntasFrecuentes> Categorias { get; set; } = new List<E_CategoriaPreguntasFrecuentes>();

        [BindProperty]
        public E_CategoriaPreguntasFrecuentes NuevaCategoria { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Categorias = _cnCategoria.Listar(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnCategoria.Registrar(NuevaCategoria, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnCategoria.Eliminar(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
