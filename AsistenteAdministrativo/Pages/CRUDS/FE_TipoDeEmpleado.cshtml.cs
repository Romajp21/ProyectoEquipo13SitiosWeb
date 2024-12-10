using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_TipoDeEmpleadoModel : PageModel
    {
        private readonly CN_TipoDeEmpleado _cnTipoDeEmpleado;

        public FE_TipoDeEmpleadoModel()
        {
            _cnTipoDeEmpleado = new CN_TipoDeEmpleado();
        }

        public List<E_TipoDeEmpleado> TiposDeEmpleado { get; set; } = new List<E_TipoDeEmpleado>();
        [BindProperty]
        public E_TipoDeEmpleado NuevoTipoDeEmpleado { get; set; }
        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            TiposDeEmpleado = _cnTipoDeEmpleado.ListarTipoDeEmpleado(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnTipoDeEmpleado.RegistrarTipoDeEmpleado(NuevoTipoDeEmpleado, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevoTipoDeEmpleado = _cnTipoDeEmpleado.ListarTipoDeEmpleado(out mensajeTemp).Find(e => e.IdTipodeEmpleado == id);
            Mensaje = mensajeTemp;
            if (NuevoTipoDeEmpleado == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnTipoDeEmpleado.EliminarTipoDeEmpleado(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
