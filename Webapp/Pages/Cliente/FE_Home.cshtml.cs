using Microsoft.AspNetCore.Mvc.RazorPages;
using CapaNegocios;
using CapaEntidades;
using System.Collections.Generic;

namespace Webapp.Pages.Cliente
{
    public class FE_HomeModel : PageModel
    {
        private readonly CN_Galeria _galeriaNegocios = new CN_Galeria();
        private readonly CN_Testimonio _testimonioNegocios = new CN_Testimonio();
        private readonly CN_Chatbox _chatboxNegocios = new CN_Chatbox();

        // Propiedades existentes
        public List<E_Galeria> ImagenesActivas { get; set; } = new List<E_Galeria>();
        public E_Galeria HeroImagen { get; set; } = new E_Galeria();
        public Dictionary<string, E_Galeria> ServiciosImagenes { get; set; } = new Dictionary<string, E_Galeria>();
        public List<E_Testimonio> TestimoniosAprobados { get; set; } = new List<E_Testimonio>();

        // Propiedades del Chatbox
        public List<E_Chatbox> PreguntasIniciales { get; set; } = new List<E_Chatbox>();
        public E_Chatbox PreguntaActual { get; set; }
        public List<E_ChatboxRespuesta> RespuestasActuales { get; set; } = new List<E_ChatboxRespuesta>();
        public string Mensaje { get; set; }

        // Método para cargar datos iniciales (OnGet)
        public void OnGet()
        {
            // Cargar datos generales
            CargarDatosGenerales();

            // Cargar preguntas iniciales para el chatbox
            CargarPreguntasIniciales();
        }


        // Método para cargar respuestas asociadas a una pregunta seleccionada
        public void OnPostSeleccionarPregunta(int idPregunta)
        {
            // Cargar los datos generales
            CargarDatosGenerales();

            if (idPregunta > 0)
            {
                // Obtener respuestas asociadas a la pregunta seleccionada
                RespuestasActuales = _chatboxNegocios.ObtenerRespuestasPorPregunta(idPregunta, out string mensajeRespuestas);
                Mensaje = mensajeRespuestas;

                if (RespuestasActuales == null || RespuestasActuales.Count == 0)
                {
                    // Si no hay respuestas, volver a las preguntas iniciales
                    Mensaje = "No hay respuestas disponibles para esta pregunta.";
                    CargarPreguntasIniciales();
                }
                else
                {
                    // Obtener la pregunta actual para mostrarla correctamente
                    PreguntaActual = _chatboxNegocios.ObtenerPreguntaPorId(idPregunta, out string mensajePregunta);

                    if (PreguntaActual == null)
                    {
                        Mensaje = "No se encontró la pregunta seleccionada.";
                        CargarPreguntasIniciales();
                    }
                }
            }
            else
            {
                Mensaje = "El ID de la pregunta seleccionada no es válido.";
                CargarPreguntasIniciales();
            }
        }


        // Método para volver a listar preguntas iniciales
        public void OnPostListarPreguntasIniciales()
        {
            CargarPreguntasIniciales();
        }

        // Método privado para cargar preguntas iniciales
        private void CargarPreguntasIniciales()
        {
            PreguntasIniciales = _chatboxNegocios.ObtenerPreguntasIniciales(out string mensaje);
            Mensaje = mensaje;

            if (PreguntasIniciales == null || PreguntasIniciales.Count == 0)
            {
                PreguntasIniciales = new List<E_Chatbox>();
                Mensaje = "No hay preguntas iniciales disponibles en este momento.";
            }

            // Asegurarse de que las respuestas actuales estén vacías
            RespuestasActuales = new List<E_ChatboxRespuesta>();
            PreguntaActual = null;
        }


        private void CargarDatosGenerales()
        {
            // Cargar testimonios aprobados
            TestimoniosAprobados = _testimonioNegocios.ObtenerTestimoniosAprobados(5, out var mensajeTestimonios);

            // Cargar imagen "hero"
            HeroImagen = _galeriaNegocios.ObtenerImagenPorDescripcion("hero", out var mensajeHero);
            if (HeroImagen == null || string.IsNullOrEmpty(HeroImagen.RutaImagen))
            {
                HeroImagen = new E_Galeria
                {
                    RutaImagen = "https://via.placeholder.com/800x400?text=Hero+Image+No+Disponible",
                    DescripcionTatuaje = "Hero Image No Disponible"
                };
            }

            // Cargar imágenes de servicios
            ServiciosImagenes.Clear(); // Limpiar el diccionario para evitar duplicados
            string[] servicios = { "pequeño", "mediano", "grande", "blanco y negro", "realismo", "ilustrativo", "tradicional" };
            foreach (var servicio in servicios)
            {
                var imagen = _galeriaNegocios.ObtenerImagenPorDescripcion(servicio, out var mensajeServicio);
                if (imagen == null || string.IsNullOrEmpty(imagen.RutaImagen))
                {
                    imagen = new E_Galeria
                    {
                        RutaImagen = "https://via.placeholder.com/150x150?text=No+Disponible",
                        DescripcionTatuaje = $"Imagen de {servicio} no disponible"
                    };
                }
                ServiciosImagenes.Add(servicio, imagen);
            }
        }

    }
}
