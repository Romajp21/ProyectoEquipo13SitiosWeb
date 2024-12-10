using CapaNegocio;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_EspecialidadesModel : PageModel
    {
        private readonly CN_Especialidades _cnEspecialidades;

        public FE_EspecialidadesModel()
        {
            _cnEspecialidades = new CN_Especialidades();
        }

        // Lista de especialidades para mostrar en la página
        public List<E_Especialidades> Especialidades { get; set; } = new List<E_Especialidades>();

        // Modelo para la nueva o editada especialidad
        [BindProperty]
        public E_Especialidades NuevaEspecialidad { get; set; }

        // Mensaje de salida para operaciones
        public string Mensaje { get; set; }

        // Método para cargar las especialidades al abrir la página
        public void OnGet()
        {
            string mensajeTemp;
            Especialidades = _cnEspecialidades.ListarEspecialidades(out mensajeTemp);
            Mensaje = mensajeTemp;
        }

        // Método para registrar una nueva especialidad
        public IActionResult OnPostRegistrar()
        {
            if (ModelState.IsValid)
            {
                string mensajeTemp;
                bool exito = _cnEspecialidades.RegistrarEspecialidad(NuevaEspecialidad, out mensajeTemp);
                TempData["Mensaje"] = mensajeTemp;
                if (exito) return RedirectToPage();
            }
            return Page();
        }

        // Método para cargar datos de una especialidad para edición
        public IActionResult OnPostActualizar(int id)
        {
            string mensajeTemp;
            NuevaEspecialidad = _cnEspecialidades.ListarEspecialidades(out mensajeTemp).Find(e => e.IdEspecialidad == id);
            Mensaje = mensajeTemp;
            if (NuevaEspecialidad == null)
            {
                TempData["Mensaje"] = mensajeTemp;
                return RedirectToPage();
            }
            return Page();
        }

        // Método para eliminar una especialidad
        public IActionResult OnPostEliminar(int id)
        {
            string mensajeTemp;
            bool exito = _cnEspecialidades.EliminarEspecialidad(id, out mensajeTemp);
            TempData["Mensaje"] = mensajeTemp;
            return RedirectToPage();
        }
    }
}
