using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_ProvinciaModel : PageModel
    {
        private readonly CN_Provincia _cnProvincia;

        public FE_ProvinciaModel()
        {
            _cnProvincia = new CN_Provincia();
        }

        public List<E_Provincia> Provincias { get; set; } = new List<E_Provincia>();

        [BindProperty]
        public E_Provincia NuevaProvincia { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Provincias = _cnProvincia.Listar(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnProvincia.Registrar(NuevaProvincia, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnProvincia.Eliminar(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
