using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Galeria
    {
        // Instancia de la capa de datos para interactuar con la base de datos
        private readonly CD_Galeria objCapaDatos = new CD_Galeria();

        // Listar todas las imágenes de la galería
        public List<E_Galeria> ListarGaleria(out string mensaje)
        {
            return objCapaDatos.ListarGaleria(out mensaje);
        }

        // Registrar una nueva imagen en la galería
        public bool RegistrarImagen(E_Galeria galeria, out string mensaje)
        {
            // Validar los datos antes de registrar
            if (!ValidarParaRegistrar(galeria, out mensaje))
                return false;

            return objCapaDatos.RegistrarImagen(galeria, out mensaje);
        }

        // Actualizar una imagen existente en la galería
        public bool ActualizarImagen(E_Galeria galeria, out string mensaje)
        {
            // Validar los datos antes de actualizar
            if (!ValidarParaActualizar(galeria, out mensaje))
                return false;

            return objCapaDatos.ActualizarImagen(galeria, out mensaje);
        }

        // Eliminar una imagen de la galería
        public bool EliminarImagen(int idImagen, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el ID de la imagen sea válido
            if (idImagen <= 0)
            {
                mensaje = "El ID de la imagen no es válido.";
                return false;
            }

            return objCapaDatos.EliminarImagen(idImagen, out mensaje);
        }

        // Validar los datos de una imagen al registrar
        private bool ValidarParaRegistrar(E_Galeria galeria, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el ID del artista sea válido
            if (galeria.IdArtista <= 0)
            {
                mensaje = "El ID del artista no es válido.";
                return false;
            }

            // Validar que la ruta de la imagen no esté vacía
            if (string.IsNullOrEmpty(galeria.RutaImagen))
            {
                mensaje = "La imagen no puede estar vacía.";
                return false;
            }

            // Validar que el contenido de la imagen esté en formato Base64
            if (!EsBase64Valido(galeria.RutaImagen))
            {
                mensaje = "La imagen no está en un formato Base64 válido.";
                return false;
            }

            return true;
        }

        // Validar los datos de una imagen al actualizar
        private bool ValidarParaActualizar(E_Galeria galeria, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el ID de la imagen sea válido
            if (galeria.IdImagen <= 0)
            {
                mensaje = "El ID de la imagen no es válido.";
                return false;
            }

            // Validar usando las mismas reglas que para registrar
            return ValidarParaRegistrar(galeria, out mensaje);
        }

        // Validar que una cadena esté en formato Base64
        private bool EsBase64Valido(string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return false;

            // Verificar que comience con el prefijo de imagen Base64
            if (!base64.StartsWith("data:image/"))
                return false;

            try
            {
                // Extraer la parte codificada después de "base64,"
                var base64Data = base64.Substring(base64.IndexOf(",") + 1);
                Convert.FromBase64String(base64Data); // Intenta decodificar
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Obtener imágenes activas
        public (List<E_Galeria>, string) ObtenerImagenesActivas()
        {
            string mensaje;
            var lista = objCapaDatos.ListarGaleriaActivas(out mensaje); // Método ajustado en la capa de datos
            return (lista, mensaje);
        }

        public E_Galeria ObtenerImagenPorDescripcion(string descripcion, out string mensaje)
        {
            return objCapaDatos.ObtenerImagenPorDescripcion(descripcion, out mensaje);
        }

    }
}
