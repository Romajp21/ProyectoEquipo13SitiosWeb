using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Para usar sesiones
using System.Collections.Generic;

namespace Artistas.Pages.CRUD
{
    public class FE_HorarioArtistaModel : PageModel
    {
        // Clase de lógica de negocios para horarios
        private readonly CN_HorarioArtista _horarioNegocios;

        // Constructor que inicializa la lógica de negocios
        public FE_HorarioArtistaModel(CN_HorarioArtista horarioNegocios)
        {
            _horarioNegocios = horarioNegocios;
        }

        // Lista de horarios del artista
        public List<E_HorarioArtista> Horarios { get; set; } = new List<E_HorarioArtista>();

        // Modelo enlazado para los datos del formulario
        [BindProperty]
        public E_HorarioArtista HorarioForm { get; set; } = new E_HorarioArtista();

        // Lista para los días seleccionados
        [BindProperty]
        public List<int> DiasSeleccionados { get; set; } = new List<int>();


        // Mensaje temporal para mostrar notificaciones al usuario
        [TempData]
        public string Mensaje { get; set; }

        // Diccionario que almacena los días de la semana
        public Dictionary<int, string> DiasSemana { get; } = new()
        {
            { 1, "Lunes" },
            { 2, "Martes" },
            { 3, "Miércoles" },
            { 4, "Jueves" },
            { 5, "Viernes" },
            { 6, "Sábado" },
            { 7, "Domingo" }
        };

        // Método que se ejecuta al cargar la página
        public void OnGet(int? idHorarioArtista)
        {
            // Recuperar el ID del artista desde la sesión (simulación por ahora)
            HorarioForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;
            HttpContext.Session.SetInt32("IdArtista", 1); // Simula un ID de artista para pruebas


            // Si se proporciona un ID de horario, cargar ese horario
            if (idHorarioArtista.HasValue)
            {
                HorarioForm = _horarioNegocios.ObtenerHorarioPorId(idHorarioArtista.Value, out string mensaje);

                // Si no se encuentra el horario, mostrar un mensaje
                if (HorarioForm == null)
                {
                    TempData["Mensaje"] = mensaje;
                    HorarioForm = new E_HorarioArtista();
                }
            }
            else
            {
                // Crear un horario nuevo con una fecha predeterminada
                HorarioForm = new E_HorarioArtista
                {
                    Fecha = DateTime.Now // Fecha por defecto al agregar un nuevo horario
                };
            }

            // Cargar los datos necesarios (lista de horarios)
            CargarDatos();
        }

        // Método para guardar (crear o actualizar) un horario
        public IActionResult OnPostGuardar()
        {
            // Recuperar el ID del artista desde la sesión
            HorarioForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            string mensaje;

            // Si es una actualización (el ID ya existe), ignorar la validación de días seleccionados
            if (HorarioForm.IdHorarioArtista > 0)
            {
                if (!_horarioNegocios.ActualizarHorario(HorarioForm, out mensaje))
                {
                    TempData["Mensaje"] = mensaje;
                    CargarDatos();
                    return Page();
                }

                TempData["Mensaje"] = "Horario actualizado exitosamente.";
            }
            else // Si es un nuevo registro
            {
                if (DiasSeleccionados == null || DiasSeleccionados.Count == 0)
                {
                    TempData["Mensaje"] = "Debe seleccionar al menos un día de la semana.";
                    CargarDatos();
                    return Page();
                }

                foreach (var dia in DiasSeleccionados)
                {
                    // Crear un horario para cada día seleccionado
                    var nuevoHorario = new E_HorarioArtista
                    {
                        IdArtista = HorarioForm.IdArtista,
                        Fecha = HorarioForm.Fecha.AddDays(dia - (int)HorarioForm.Fecha.DayOfWeek), // Calcula la fecha para el día seleccionado
                        HoraInicio = HorarioForm.HoraInicio,
                        HoraFin = HorarioForm.HoraFin,
                        Estado = HorarioForm.Estado
                    };

                    if (!_horarioNegocios.RegistrarHorario(nuevoHorario, out mensaje))
                    {
                        TempData["Mensaje"] = mensaje;
                        CargarDatos();
                        return Page();
                    }
                }

                TempData["Mensaje"] = "Horarios registrados exitosamente.";
            }

            return RedirectToPage();
        }



        // Método para eliminar un horario
        public IActionResult OnPostEliminar(int id)
        {
            string mensaje;

            // Intentar eliminar el horario
            if (_horarioNegocios.EliminarHorario(id, out mensaje))
            {
                TempData["Mensaje"] = "Horario eliminado exitosamente.";
            }
            else
            {
                TempData["Mensaje"] = mensaje;
            }

            // Redirigir para actualizar la tabla
            return RedirectToPage();
        }

        // Método para cargar datos de un horario específico para editar
        public IActionResult OnPostEditar()
        {
            // Recuperar el ID del artista desde la sesión
            HorarioForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            // Buscar el horario en la lista actual
            HorarioForm = Horarios.Find(h => h.IdHorarioArtista == HorarioForm.IdHorarioArtista) ?? new E_HorarioArtista();
            DiasSeleccionados = new List<int> { (int)HorarioForm.Fecha.DayOfWeek };

            // Actualizar datos
            CargarDatos();

            // Mostrar la página con los datos cargados
            return Page();
        }

        // Método para cargar la lista de horarios desde la lógica de negocios
        private void CargarDatos()
        {
            // Recuperar el ID del artista desde la sesión
            HorarioForm.IdArtista = HttpContext.Session.GetInt32("IdArtista") ?? 0;

            // Obtener los horarios del artista
            Horarios = _horarioNegocios.ListarHorarios(HorarioForm.IdArtista, out var mensaje);
        }

        // Método para renderizar dinámicamente el estado en el formulario
        public string RenderDropdownEstado()
        {
            return $@"
                <option value=""true"" {(HorarioForm.Estado ? "selected" : "")}>Activo</option>
                <option value=""false"" {(!HorarioForm.Estado ? "selected" : "")}>Inactivo</option>";
        }
    }
}
