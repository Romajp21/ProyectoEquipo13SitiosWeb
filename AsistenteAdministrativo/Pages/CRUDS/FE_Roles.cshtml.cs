using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_RolesModel : PageModel
    {
        private readonly CN_Roles _cnRoles;

        public FE_RolesModel(CN_Roles cnRoles) // Inyección de dependencia
        {
            _cnRoles = cnRoles;
        }

        // Lista de roles para mostrar en la página
        public List<E_Roles> Roles { get; set; } = new List<E_Roles>();

        // Modelo para el rol nuevo o editado
        [BindProperty]
        public E_Roles NuevoRol { get; set; }

        // Mensaje de salida para las operaciones
        public string Mensaje { get; set; }

        // Método para cargar los roles al abrir la página
        public void OnGet()
        {
            string mensajeTemp;
            Roles = _cnRoles.ListarRoles(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        // Método para registrar un nuevo rol
        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnRoles.RegistrarRol(NuevoRol, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        // Método para cargar los datos de un rol para edición
        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevoRol = _cnRoles.ListarRoles(out mensajeTemp).Find(r => r.IdRole == id);
            Mensaje = mensajeTemp;
            if (NuevoRol == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        // Método para eliminar un rol
        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnRoles.EliminarRol(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
