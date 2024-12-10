using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Para usar sesiones
using System.Collections.Generic;

namespace Artistas.Pages.CRUD
{
    public class FE_AgendaArtistaModel : PageModel
    {
        private readonly CN_AgendaArtista _agendaNegocios;
        private readonly CN_HorarioArtista _horarioNegocios;

        public FE_AgendaArtistaModel(CN_AgendaArtista agendaNegocios, CN_HorarioArtista horarioNegocios)
        {
            _agendaNegocios = agendaNegocios;
            _horarioNegocios = horarioNegocios;
        }

        public List<E_AgendaArtista> Agenda { get; set; } = new List<E_AgendaArtista>();
        public List<E_HorarioArtista> HorariosArtista { get; set; } = new List<E_HorarioArtista>();

        [BindProperty]
        public E_AgendaArtista AgendaForm { get; set; } = new E_AgendaArtista();

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            // Establecer manualmente el IdArtista en la sesión (simulación)
            HttpContext.Session.SetInt32("IdArtista", 1); // Cambia "1" por el valor que necesites

            // Recuperar el IdArtista desde la sesión
            AgendaForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            CargarDatos();
        }

        public IActionResult OnPostGuardar()
        {
            // Recuperar el IdArtista desde la sesión
            AgendaForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            string mensaje;

            // Verificar si es un registro nuevo o una edición
            if (AgendaForm.IdAgendaArtista > 0) // Editar
            {
                // Asegúrate de que no se cree una nueva entrada al editar
                if (_agendaNegocios.ActualizarAgenda(AgendaForm, out mensaje))
                {
                    Mensaje = "Agenda actualizada exitosamente.";
                }
                else
                {
                    Mensaje = mensaje;
                }
            }
            else // Registrar
            {
                if (_agendaNegocios.RegistrarAgenda(AgendaForm, out mensaje))
                {
                    Mensaje = "Agenda registrada exitosamente.";
                }
                else
                {
                    Mensaje = mensaje;
                }
            }

            // Recargar la lista de agendas después de guardar
            CargarDatos();

            return Page(); // O `RedirectToPage()` si quieres limpiar el formulario
        }



        public IActionResult OnPostEditar()
        {
            // Recuperar el IdArtista desde la sesión
            AgendaForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            // Validar si el ID de la agenda es válido
            if (AgendaForm.IdAgendaArtista <= 0)
            {
                Mensaje = "El ID de la agenda no es válido.";
                return RedirectToPage();
            }

            // Cargar la agenda seleccionada directamente desde la base de datos
            var agendaSeleccionada = _agendaNegocios.ListarAgenda(
                AgendaForm.IdArtista,
                null, // El ID del horario puede ser nulo porque buscamos por IdAgendaArtista
                null, // Fecha no necesaria
                out var mensaje)
                .Find(a => a.IdAgendaArtista == AgendaForm.IdAgendaArtista);

            if (agendaSeleccionada != null)
            {
                // Asignar los valores al formulario
                AgendaForm = agendaSeleccionada;

                // Cargar los horarios asociados al artista
                CargarHorarios();

                return Page(); // Mostrar el formulario con los datos cargados
            }

            Mensaje = "No se encontró la agenda seleccionada.";
            return RedirectToPage(); // Regresar en caso de error
        }



        public IActionResult OnPostEliminar()
        {
            string mensaje;

            // Verificar si el ID de la agenda es válido
            if (AgendaForm.IdAgendaArtista <= 0)
            {
                Mensaje = "El ID de la agenda no es válido para eliminar.";
                return RedirectToPage();
            }

            // Intentar eliminar la agenda
            if (_agendaNegocios.EliminarAgenda(AgendaForm.IdAgendaArtista, out mensaje))
            {
                Mensaje = "Agenda eliminada exitosamente.";
            }
            else
            {
                Mensaje = mensaje;
            }

            return RedirectToPage();
        }

        private void CargarDatos()
        {
            // Recuperar el IdArtista desde la sesión
            AgendaForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            // Listar la agenda para el artista
            Agenda = _agendaNegocios.ListarAgenda(AgendaForm.IdArtista, null, null, out var mensaje);

            if (AgendaForm.IdArtista > 0)
            {
                CargarHorarios();
            }
        }

        private void CargarHorarios()
        {
            // Recuperar los horarios disponibles para el artista
            HorariosArtista = _horarioNegocios.ListarHorarios(AgendaForm.IdArtista, out var mensajeHorarios);
        }

        public IActionResult OnGetCargarAgenda(int idAgenda)
        {
            var agenda = _agendaNegocios.ObtenerAgendaPorId(idAgenda, out var mensaje);
            if (agenda != null)
            {
                return new JsonResult(new
                {
                    idAgenda = agenda.IdAgendaArtista,
                    idArtista = agenda.IdArtista,
                    idHorarioArtista = agenda.IdHorarioArtista,
                    fecha = agenda.Fecha.ToString("yyyy-MM-dd"),
                    horaInicio = agenda.HoraInicio.ToString(@"hh\:mm"),
                    horaFin = agenda.HoraFin.ToString(@"hh\:mm"),
                    estado = agenda.Estado
                });
            }

            return new JsonResult(new { error = mensaje });
        }



    }
}
