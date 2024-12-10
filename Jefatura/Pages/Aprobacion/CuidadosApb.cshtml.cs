using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Jefatura.Pages.Aprobacion
{
    public class CuidadosApbModel : PageModel
    {
        private readonly CN_CuidadosDelTatuaje _cnCuidados;

        public CuidadosApbModel(IConfiguration configuration)
        {
            _cnCuidados = new CN_CuidadosDelTatuaje(configuration);
        }

        public List<E_CuidadosDelTatuaje> ListaCuidados { get; set; } = new();

        [BindProperty]
        public E_CuidadosDelTatuaje CuidadoEditar { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? IdCuidadoEliminar { get; set; }

        public void OnGet()
        {
            ListaCuidados = _cnCuidados.ObtenerCuidadosDelTatuaje();
        }

        public IActionResult OnPostEditarJefatura()
        {
            if (CuidadoEditar != null && CuidadoEditar.IdCuidadosDelTatuaje > 0)
            {
                try
                {
                    _cnCuidados.EditarCuidadoDelTatuajeJefatura(CuidadoEditar);
                    TempData["Mensaje"] = "Cuidado actualizado correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al actualizar el cuidado: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Datos inválidos para el cuidado.";
            }

            return RedirectToPage();
        }


        public IActionResult OnPostEliminar()
        {
            if (IdCuidadoEliminar.HasValue)
            {
                try
                {
                    // Eliminar cuidado
                    _cnCuidados.EliminarCuidadoDelTatuaje(IdCuidadoEliminar.Value);
                    TempData["Mensaje"] = "Cuidado eliminado correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error al eliminar el cuidado: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "No se pudo identificar el cuidado a eliminar.";
            }

            return RedirectToPage();
        }
    }
}
