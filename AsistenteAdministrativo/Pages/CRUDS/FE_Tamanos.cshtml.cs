using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_TamanosModel : PageModel
    {
        private readonly CN_Tamanos _cnTamanos;

        public FE_TamanosModel()
        {
            _cnTamanos = new CN_Tamanos();
        }

        public List<E_Tamanos> Tamanos { get; set; } = new List<E_Tamanos>();

        [BindProperty]
        public E_Tamanos NuevoTamano { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Tamanos = _cnTamanos.ListarTamanos(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnTamanos.RegistrarTamano(NuevoTamano, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevoTamano = _cnTamanos.ListarTamanos(out mensajeTemp).Find(t => t.IdTamano == id);
            Mensaje = mensajeTemp;
            if (NuevoTamano == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnTamanos.EliminarTamano(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
