using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_GaleriaCategorias
    {
        // Instancia de la capa de datos para interactuar con la base de datos
        private readonly CD_GaleriaCategorias objCapaDatos = new CD_GaleriaCategorias();

        // Listar todas las categorías asociadas a las imágenes de la galería
        public List<E_GaleriaCategorias> ListarGaleriaCategorias(out string mensaje)
        {
            return objCapaDatos.ListarGaleriaCategorias(out mensaje);
        }

        // Listar todas las categorías disponibles
        public List<E_CategoriaTatuajes> ListarCategorias(out string mensaje)
        {
            return objCapaDatos.ListarCategorias(out mensaje);
        }

        // Registrar una nueva categoría asociada a una imagen
        public bool RegistrarGaleriaCategoria(E_GaleriaCategorias galeriaCategoria, out string mensaje)
        {
            // Validar los datos antes de registrar
            if (!ValidarGaleriaCategoria(galeriaCategoria, out mensaje))
                return false;

            return objCapaDatos.RegistrarGaleriaCategoria(galeriaCategoria, out mensaje);
        }

        // Actualizar una categoría asociada a una imagen
        public bool ActualizarGaleriaCategoria(E_GaleriaCategorias galeriaCategoria, out string mensaje)
        {
            // Validar los datos antes de actualizar
            if (!ValidarGaleriaCategoria(galeriaCategoria, out mensaje))
                return false;

            return objCapaDatos.ActualizarGaleriaCategoria(galeriaCategoria, out mensaje);
        }

        // Eliminar una categoría asociada a una imagen
        public bool EliminarGaleriaCategoria(int idImagen, int idCategoriaTatuajes, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que ambos IDs sean válidos
            if (idImagen <= 0 || idCategoriaTatuajes <= 0)
            {
                mensaje = "El ID de la imagen o de la categoría no es válido.";
                return false;
            }

            return objCapaDatos.EliminarGaleriaCategoria(idImagen, idCategoriaTatuajes, out mensaje);
        }

        // Validar los datos de una categoría asociada a una imagen
        private bool ValidarGaleriaCategoria(E_GaleriaCategorias galeriaCategoria, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el ID de la imagen sea mayor a cero
            if (galeriaCategoria.IdImagen <= 0)
            {
                mensaje = "El ID de la imagen no es válido.";
                return false;
            }

            // Validar que el ID de la categoría sea mayor a cero
            if (galeriaCategoria.IdCategoriaTatuajes <= 0)
            {
                mensaje = "El ID de la categoría no es válido.";
                return false;
            }

            return true;
        }
    }
}
