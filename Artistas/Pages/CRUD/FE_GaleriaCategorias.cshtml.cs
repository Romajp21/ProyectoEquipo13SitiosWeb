using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaNegocio;

namespace Artistas.Pages.CRUD
{
    public class FE_GaleriaCategoriasModel : PageModel
    {
        private readonly CN_GaleriaCategorias _galeriaCategoriasNegocio;
        private readonly CN_Galeria _galeriaNegocio;
        private readonly CN_CategoriaTatuajes _categoriaNegocio;

        public FE_GaleriaCategoriasModel(CN_GaleriaCategorias galeriaCategoriasNegocio, CN_Galeria galeriaNegocio, CN_CategoriaTatuajes categoriaNegocio)
        {
            _galeriaCategoriasNegocio = galeriaCategoriasNegocio;
            _galeriaNegocio = galeriaNegocio;
            _categoriaNegocio = categoriaNegocio;
        }

        public List<E_GaleriaCategorias> GaleriaCategorias { get; set; }
        public List<E_Galeria> ListaGaleria { get; set; }
        public List<E_CategoriaTatuajes> ListaCategorias { get; set; }

        [BindProperty]
        public E_GaleriaCategorias GaleriaCategoriaForm { get; set; } = new E_GaleriaCategorias();

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet(int? idImagen, int? idCategoria)
        {
            CargarDatos();

            if (idImagen.HasValue && idCategoria.HasValue)
            {
                GaleriaCategoriaForm = GaleriaCategorias.Find(g => g.IdImagen == idImagen.Value && g.IdCategoriaTatuajes == idCategoria.Value);
            }
        }

        public IActionResult OnPostGuardar()
        {
            string mensaje;
            if (GaleriaCategoriaForm.IdImagen == 0 || GaleriaCategoriaForm.IdCategoriaTatuajes == 0)
            {
                Mensaje = "Debe seleccionar una imagen y una categoría.";
                return Page();
            }

            bool exito = _galeriaCategoriasNegocio.RegistrarGaleriaCategoria(GaleriaCategoriaForm, out mensaje);
            Mensaje = exito ? "Relación guardada exitosamente." : mensaje;
            return RedirectToPage();
        }

        public IActionResult OnPostActualizar()
        {
            string mensaje;
            bool exito = _galeriaCategoriasNegocio.ActualizarGaleriaCategoria(GaleriaCategoriaForm, out mensaje);
            Mensaje = exito ? "Relación actualizada exitosamente." : mensaje;
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            string mensaje;
            bool exito = _galeriaCategoriasNegocio.EliminarGaleriaCategoria(GaleriaCategoriaForm.IdImagen, GaleriaCategoriaForm.IdCategoriaTatuajes, out mensaje);
            Mensaje = exito ? "Relación eliminada exitosamente." : mensaje;
            return RedirectToPage();
        }

        private void CargarDatos()
        {
            string mensaje;
            GaleriaCategorias = _galeriaCategoriasNegocio.ListarGaleriaCategorias(out mensaje);
            ListaGaleria = _galeriaNegocio.ListarGaleria(out mensaje);
           // ListaCategorias = _categoriaNegocio.ListarCategorias(out mensaje);
        }

        public string RenderDropdownImagenes()
        {
            var sb = new StringBuilder();
            sb.Append("<option value=\"\">Seleccione una imagen</option>");
            foreach (var galeria in ListaGaleria)
            {
                sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>",
                    galeria.IdImagen,
                    galeria.IdImagen == GaleriaCategoriaForm.IdImagen ? "selected" : "",
                    galeria.DescripcionTatuaje);
            }
            return sb.ToString();
        }

        public string RenderDropdownCategorias()
        {
            var sb = new StringBuilder();
            sb.Append("<option value=\"\">Seleccione una categoría</option>");
            foreach (var categoria in ListaCategorias)
            {
                sb.AppendFormat("<option value=\"{0}\" {1}>{2}</option>",
                    categoria.IdCategoriaTatuajes,
                    categoria.IdCategoriaTatuajes == GaleriaCategoriaForm.IdCategoriaTatuajes ? "selected" : "",
                    categoria.NombreCategoria);
            }
            return sb.ToString();
        }
        public string RenderDropdownEstado()
        {
            return $@"
        <option value=""true"" {(GaleriaCategoriaForm.Estado ? "selected" : "")}>Activo</option>
        <option value=""false"" {(!GaleriaCategoriaForm.Estado ? "selected" : "")}>Inactivo</option>";
        }

    }
}
