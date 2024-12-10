using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_CalendarioModel : PageModel
    {
        private readonly CN_Calendario _cnCalendario;

        public FE_CalendarioModel()
        {
            _cnCalendario = new CN_Calendario();
        }

        public List<E_Calendario> Calendario { get; set; } = new List<E_Calendario>();

        [BindProperty]
        public E_Calendario NuevoBloque { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Calendario = _cnCalendario.ListarCalendario(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnCalendario.RegistrarBloque(NuevoBloque, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevoBloque = _cnCalendario.ListarCalendario(out mensajeTemp).Find(b => b.IdBloque == id);
            Mensaje = mensajeTemp;
            if (NuevoBloque == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnCalendario.EliminarBloque(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
