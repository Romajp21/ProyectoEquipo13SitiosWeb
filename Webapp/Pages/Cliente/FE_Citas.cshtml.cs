using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Para usar sesiones
using System;
using System.Collections.Generic;
using System.Linq;

namespace Webapp.Pages.Cliente
{
    public class FE_CitasModel : PageModel
    {
        private readonly CN_Citas _citasNegocios;

        public FE_CitasModel(CN_Citas citasNegocios)
        {
            _citasNegocios = citasNegocios;
        }

        // Lista de artistas disponibles
        public List<E_Artista> ArtistasDisponibles { get; set; } = new List<E_Artista>();

        // Lista de fechas y horarios disponibles
        public List<E_DisponibilidadCitas> FechasDisponibles { get; set; } = new List<E_DisponibilidadCitas>();

        // Lista de citas registradas
        public List<E_Citas> Citas { get; set; } = new List<E_Citas>();

        // Propiedad de enlace para el formulario de cita
        [BindProperty]
        public E_Citas CitaForm { get; set; } = new E_Citas();

        // Mensaje temporal para mostrar alertas
        [TempData]
        public string Mensaje { get; set; }

        // Método que se ejecuta al cargar la página
        public void OnGet(int? idArtista)
        {
            // Simular inicio de sesión del cliente (IDCliente = 1 para pruebas)
            HttpContext.Session.SetInt32("IdCliente", 1);
            CitaForm.IdCliente = HttpContext.Session.GetInt32("IdCliente") ?? 0;

            if (CitaForm.IdCliente > 0)
            {
                // Obtener la lista de artistas disponibles
                ArtistasDisponibles = _citasNegocios.ObtenerArtistas(out _);

                // Si idArtista es 0 (Cualquier Artista), asignar null
                if (idArtista == 0)
                {
                    idArtista = null;
                }

                // Establecer el IdArtista en el modelo
                CitaForm.IdArtista = idArtista;

                // Generar fechas disponibles según el artista seleccionado
                GenerarFechasDisponibles(idArtista);
                // Cargar citas del cliente
                string mensaje;
                Citas = _citasNegocios.ListarCitas(CitaForm.IdCliente, null, out mensaje);

                if (!string.IsNullOrEmpty(mensaje))
                {
                    Mensaje = mensaje;
                }
            }

            // Guardar en TempData si el modal debe estar abierto
            if (Request.Query.ContainsKey("modal"))
            {
                TempData["AbrirModal"] = true;
            }
        }



        // Método que se ejecuta al guardar una cita
        public IActionResult OnPostGuardar()
            {
            CitaForm.IdCliente = HttpContext.Session.GetInt32("IdCliente") ?? 0;

            Console.WriteLine($"HoraInicio: {CitaForm.HoraInicio}");
            Console.WriteLine($"HoraFin: {CitaForm.HoraFin}");

            if (CitaForm.HoraInicio == TimeSpan.Zero || CitaForm.HoraFin == TimeSpan.Zero)
            {
                Mensaje = "Debe seleccionar un horario válido.";
                TempData["AbrirModal"] = true;
                return RedirectToPage();
            }

            // Registro de la cita
            string mensaje;
            if (_citasNegocios.RegistrarCita(CitaForm, out mensaje))
            {
                Mensaje = "Cita registrada exitosamente.";
            }
            else
            {
                Mensaje = mensaje;
                TempData["AbrirModal"] = true;
            }

            return RedirectToPage();
        }




        // Método para generar las fechas disponibles según el artista
        private void GenerarFechasDisponibles(int? idArtista)
        {
            string mensaje;

            // Obtener horarios disponibles para el artista o todos los artistas si no se selecciona ninguno

            var disponibilidad = _citasNegocios.ObtenerHorariosDisponibles(idArtista, 6, 3, out mensaje);

            // Transformar la lista de disponibilidad en la lista utilizada por la vista
            FechasDisponibles = disponibilidad.Select(d => new E_DisponibilidadCitas
            {
                IdArtista = d.IdArtista,
                IdHorarioArtista = d.IdHorarioArtista, // Agregar esta línea
                Fecha = d.Fecha,
                HoraInicio = d.HoraInicio,
                HoraFin = d.HoraFin
            }).ToList();
        }


        public IActionResult OnPostEliminar(int idCita)
        {
            string mensaje;

            if (_citasNegocios.EliminarCita(idCita, out mensaje))
            {
                TempData["Mensaje"] = "Cita eliminada exitosamente.";
            }
            else
            {
                TempData["Mensaje"] = $"Error al eliminar la cita: {mensaje}";
            }

            return RedirectToPage(); // Recarga la página para reflejar los cambios
        }


        public IActionResult OnPostEditar(int idCita)
        {
            // Capturar el ID del cliente desde la sesión
            CitaForm.IdCliente = HttpContext.Session.GetInt32("IdCliente") ?? 0;

            if (CitaForm.IdCliente == 0)
            {
                Mensaje = "Debe iniciar sesión para editar una cita.";
                return RedirectToPage();
            }

            // Obtener la cita por ID
            string mensaje;
            var cita = _citasNegocios.ListarCitas(idCita, null, out mensaje).FirstOrDefault();

            if (cita == null)
            {
                Mensaje = "No se encontró la cita seleccionada.";
                return RedirectToPage();
            }

            // Cargar los datos de la cita en el modelo
            CitaForm = cita;
            Mensaje = "Cita cargada para edición.";
            return RedirectToPage();
        }

    }
}