using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_PartesDelCuerpoModel : PageModel
    {
        private readonly CN_PartesDelCuerpo _cnPartesDelCuerpo;

        public FE_PartesDelCuerpoModel()
        {
            _cnPartesDelCuerpo = new CN_PartesDelCuerpo();
        }

        public List<E_PartesDelCuerpo> PartesDelCuerpo { get; set; } = new List<E_PartesDelCuerpo>();

        [BindProperty]
        public E_PartesDelCuerpo NuevaParteDelCuerpo { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            PartesDelCuerpo = _cnPartesDelCuerpo.ListarPartesDelCuerpo(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnPartesDelCuerpo.RegistrarParteDelCuerpo(NuevaParteDelCuerpo, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevaParteDelCuerpo = _cnPartesDelCuerpo.ListarPartesDelCuerpo(out mensajeTemp).Find(p => p.IdParteDelCuerpo == id);
            Mensaje = mensajeTemp;
            if (NuevaParteDelCuerpo == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnPartesDelCuerpo.EliminarParteDelCuerpo(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
