using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_DistritoModel : PageModel
    {
        private readonly CN_Distrito _cnDistrito;
        private readonly CN_Canton _cnCanton;

        public FE_DistritoModel()
        {
            _cnDistrito = new CN_Distrito();
            _cnCanton = new CN_Canton();
        }

        public List<E_Distrito> Distritos { get; set; } = new List<E_Distrito>();
        public List<E_Canton> Cantones { get; set; } = new List<E_Canton>();

        [BindProperty]
        public E_Distrito NuevoDistrito { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Distritos = _cnDistrito.Listar(out mensajeTemp);
            Cantones = _cnCanton.Listar(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnDistrito.Registrar(NuevoDistrito, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnDistrito.Eliminar(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }

        public string ObtenerNombreCanton(int idCanton)
        {
            var canton = Cantones.Find(c => c.IdCanton == idCanton);
            return canton != null ? canton.NombreCanton : "Desconocido";
        }
    }
}
