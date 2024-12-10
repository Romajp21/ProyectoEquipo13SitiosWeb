using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_CantonModel : PageModel
    {
        private readonly CN_Canton _cnCanton;
        private readonly CN_Provincia _cnProvincia;

        public FE_CantonModel()
        {
            _cnCanton = new CN_Canton();
            _cnProvincia = new CN_Provincia();
        }

        public List<E_Canton> Cantones { get; set; } = new List<E_Canton>();
        public List<E_Provincia> Provincias { get; set; } = new List<E_Provincia>();

        [BindProperty]
        public E_Canton NuevoCanton { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            string mensajeTemp;
            Cantones = _cnCanton.Listar(out mensajeTemp);
            Provincias = _cnProvincia.Listar(out _);
            Mensaje = mensajeTemp;
        }

        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnCanton.Registrar(NuevoCanton, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnCanton.Eliminar(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }

        public string ObtenerNombreProvincia(int idProvincia)
        {
            var provincia = Provincias.Find(p => p.IdProvincia == idProvincia);
            return provincia != null ? provincia.NombreProvincia : "Desconocido";
        }
    }
}
