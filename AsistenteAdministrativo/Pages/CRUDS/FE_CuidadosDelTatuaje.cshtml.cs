using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class CRUD_CuidadosTatuajeModel : PageModel
    {
        private readonly CN_CuidadosDelTatuaje _cnCuidados;

        public CRUD_CuidadosTatuajeModel(IConfiguration configuration)
        {
            _cnCuidados = new CN_CuidadosDelTatuaje(configuration);
        }

        public List<E_CuidadosDelTatuaje> ListaCuidados { get; set; } = new();

        [BindProperty]
        public E_CuidadosDelTatuaje NuevoCuidado { get; set; }

        [BindProperty]
        public E_CuidadosDelTatuaje CuidadoEditar { get; set; }

        [BindProperty]
        public int? IdCuidadoEliminar { get; set; }

        [BindProperty]
        public IFormFile Imagen { get; set; }

        public void OnGet()
        {
            ListaCuidados = _cnCuidados.ObtenerCuidadosDelTatuaje();
        }

        public IActionResult OnPostCrear()
        {
            if (NuevoCuidado != null && !string.IsNullOrWhiteSpace(NuevoCuidado.DescripcionCuidadosDelTatuaje))
            {
                if (Imagen != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Imagen.CopyTo(ms);
                        var imageBytes = ms.ToArray();
                        NuevoCuidado.Imagen = Convert.ToBase64String(imageBytes);
                    }
                }

                _cnCuidados.InsertarCuidadoDelTatuaje(NuevoCuidado);
                return RedirectToPage("./FE_CuidadosDelTatuaje");
            }

            return Page();
        }

        public IActionResult OnPostEditar()
        {
            if (CuidadoEditar != null && CuidadoEditar.IdCuidadosDelTatuaje > 0)
            {
                if (Imagen != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Imagen.CopyTo(ms);
                        var imageBytes = ms.ToArray();
                        CuidadoEditar.Imagen = Convert.ToBase64String(imageBytes);
                    }
                }

                _cnCuidados.EditarCuidadoDelTatuaje(CuidadoEditar);
                return RedirectToPage("./FE_CuidadosDelTatuaje");
            }

            return Page();
        }

        public IActionResult OnPostEliminar()
        {
            if (IdCuidadoEliminar.HasValue)
            {
                _cnCuidados.EliminarCuidadoDelTatuaje(IdCuidadoEliminar.Value);
                return RedirectToPage("./FE_CuidadosDelTatuaje");
            }

            return Page();
        }
    }
}
