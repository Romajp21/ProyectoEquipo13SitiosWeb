using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace Artistas.Pages.CRUD
{
    public class FE_GaleriaModel : PageModel
    {
        // Instancia de la capa de negocios para manejar la lógica relacionada con la galería
        private readonly CN_Galeria _galeriaNegocios = new CN_Galeria();

        // Modelo que representa los datos de una galería (imagen)
        [BindProperty]
        public E_Galeria NuevaGaleria { get; set; } = new E_Galeria();

        // Archivo de imagen que el usuario sube mediante el formulario
        [BindProperty]
        public IFormFile ArchivoImagen { get; set; }

        // Lista para almacenar y mostrar todas las imágenes registradas
        public List<E_Galeria> ListaGalerias { get; set; } = new List<E_Galeria>();

        // Mensaje que se utiliza para mostrar información o errores al usuario
        public string Mensaje { get; set; }

        // Método que maneja solicitudes GET. Se usa para cargar datos iniciales.
        public void OnGet(int? id)
        {
            // Si hay un ID proporcionado, estamos en modo de edición
            if (id.HasValue)
            {
                // Buscar la imagen correspondiente al ID
                var galeriaExistente = _galeriaNegocios.ListarGaleria(out var mensaje).Find(g => g.IdImagen == id.Value);
                if (galeriaExistente != null)
                {
                    NuevaGaleria = galeriaExistente; // Cargar los datos existentes, incluyendo RutaImagen
                }
                else
                {
                    Mensaje = "No se encontró la imagen seleccionada.";
                }
            }
            else
            {
                // Si no hay ID, es una nueva imagen
                NuevaGaleria = new E_Galeria();
            }

            // Cargar todas las imágenes registradas
            ListaGalerias = _galeriaNegocios.ListarGaleria(out _);
        }
        // Método que maneja solicitudes POST para registrar o actualizar imágenes
        public IActionResult OnPost()
        {
            bool resultado;

            // Si no hay un ID, registramos una nueva imagen
            if (NuevaGaleria.IdImagen == 0)
            {
                resultado = RegistrarImagen();
            }
            else
            {
                // Si ya existe un ID, actualizamos la imagen
                resultado = ActualizarImagen();
            }

            // Si la operación fue exitosa, redirigimos al formulario limpio
            if (resultado)
                return RedirectToPage(new { id = (int?)null });

            // Si hubo un error, permanecemos en la misma página y mostramos el mensaje
            return Page();
        }

        // Método que maneja la eliminación de una imagen
        public IActionResult OnPostEliminar(int id)
        {
            // Intentar eliminar la imagen usando su ID
            var resultado = _galeriaNegocios.EliminarImagen(id, out var mensaje);
            Mensaje = mensaje;

            // Actualizar la lista de imágenes registradas
            ListaGalerias = _galeriaNegocios.ListarGaleria(out _);

            // Mostrar un mensaje según el resultado de la eliminación
            TempData["Mensaje"] = resultado ? "Imagen eliminada exitosamente." : "Error al eliminar la imagen.";
            return Page();
        }

        // Método para registrar una nueva imagen
        private bool RegistrarImagen()
        {
            // Validar que se haya subido un archivo de imagen
            if (ArchivoImagen == null || ArchivoImagen.Length == 0)
            {
                Mensaje = "Por favor, sube una imagen válida.";
                return false;
            }

            try
            {
                // Convertir la imagen subida a formato Base64
                using (var ms = new MemoryStream())
                {
                    ArchivoImagen.CopyTo(ms);
                    var base64 = $"data:{ArchivoImagen.ContentType};base64,{System.Convert.ToBase64String(ms.ToArray())}";
                    NuevaGaleria.RutaImagen = base64;
                }

                // Registrar la imagen en la base de datos
                var resultado = _galeriaNegocios.RegistrarImagen(NuevaGaleria, out var mensaje);
                Mensaje = mensaje;
                return resultado;
            }
            catch (IOException ex)
            {
                Mensaje = $"Error al procesar la imagen: {ex.Message}";
                return false;
            }
        }

        // Método para actualizar una imagen existente
        private bool ActualizarImagen()
        {
            try
            {
                // Si no se sube una nueva imagen, conservar la ruta existente
                if (ArchivoImagen == null || ArchivoImagen.Length == 0)
                {
                    var galeriaExistente = _galeriaNegocios.ListarGaleria(out _)
                        .Find(g => g.IdImagen == NuevaGaleria.IdImagen);

                    if (galeriaExistente != null)
                    {
                        NuevaGaleria.RutaImagen = galeriaExistente.RutaImagen; // Mantener la ruta existente
                    }
                }
                else
                {
                    // Si se sube una nueva imagen, convertirla a Base64
                    using (var ms = new MemoryStream())
                    {
                        ArchivoImagen.CopyTo(ms);
                        var base64 = $"data:{ArchivoImagen.ContentType};base64,{System.Convert.ToBase64String(ms.ToArray())}";
                        NuevaGaleria.RutaImagen = base64;
                    }
                }

                // Actualizar la imagen en la base de datos
                var resultado = _galeriaNegocios.ActualizarImagen(NuevaGaleria, out var mensaje);
                Mensaje = mensaje;

                return resultado;
            }
            catch (IOException ex)
            {
                Mensaje = $"Error al actualizar la imagen: {ex.Message}";
                return false;
            }
        }

    }
}
