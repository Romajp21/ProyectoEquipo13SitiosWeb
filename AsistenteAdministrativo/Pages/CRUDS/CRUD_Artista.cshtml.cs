using CapaEntidades;
using CapaNegocio;
using CapaNegocios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class CRUD_ArtistaModel : PageModel
    {
        private readonly CN_Artista _cnArtista;
        private readonly CN_Roles _cnRoles;
        private readonly CN_Especialidades _cnEspecialidades;

        public CRUD_ArtistaModel(IConfiguration configuration)
        {
            _cnArtista = new CN_Artista(configuration);
            _cnRoles = new CN_Roles();
            _cnEspecialidades = new CN_Especialidades();
        }

        public List<E_Artista> ListaArtistas { get; set; } = new();
        public List<E_Roles> ListaRoles { get; set; } = new();
        public List<E_Especialidades> ListaEspecialidades { get; set; } = new();

        [BindProperty]
        public E_Artista NuevoArtista { get; set; }

        [BindProperty]
        public E_Artista ArtistaEditar { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? IdArtistaEliminar { get; set; }

        public string Mensaje { get; set; }

        public void OnGet()
        {
            try
            {
                // Cargar artistas, roles y especialidades
                ListaArtistas = _cnArtista.ObtenerArtistas(); // Sin filtro por estado
                ListaRoles = _cnRoles.ListarRoles(out string mensajeRoles);
                ListaEspecialidades = _cnEspecialidades.ListarEspecialidades(out string mensajeEspecialidades);

                Console.WriteLine("Cargando datos desde la base de datos...");
                Console.WriteLine($"Roles disponibles: {ListaRoles.Count}");
                Console.WriteLine($"Especialidades disponibles: {ListaEspecialidades.Count}");

                foreach (var artista in ListaArtistas)
                {
                    // Obtener especialidades asignadas
                    artista.Especialidades = _cnArtista.ObtenerEspecialidadesPorArtista(artista.IdArtista)
                                                        .Select(e => e.NombreEspecialidad)
                                                        .ToList();

                    // Vincular roles
                    var rol = ListaRoles.FirstOrDefault(r => r.IdRole == artista.IdRole);
                    artista.Rol = rol?.Role ?? "Sin Rol";
                }

                Console.WriteLine($"Artistas cargados: {ListaArtistas.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos: {ex.Message}");
                Mensaje = "Error al cargar los datos. Revisa la configuración o las consultas.";
            }
        }


        public IActionResult OnPostCrear()
        {
            if (NuevoArtista == null || string.IsNullOrWhiteSpace(NuevoArtista.Nombre) || string.IsNullOrWhiteSpace(NuevoArtista.Correo))
            {
                Mensaje = "Error: Datos inválidos para crear un artista.";
                CargarDatos();
                return Page();
            }

            try
            {
                NuevoArtista.Estado = false;
                // Crear el artista y obtener el ID generado
                int nuevoIdArtista = _cnArtista.CrearArtista(NuevoArtista);

                // Asignar el ID generado al objeto
                NuevoArtista.IdArtista = nuevoIdArtista;

                Mensaje = "Artista creado exitosamente.";
                return RedirectToPage("./CRUD_Artista"); // Redirigir para actualizar la tabla
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al crear el artista: {ex.Message}";
                CargarDatos();
                return Page();
            }
        }




        public IActionResult OnPostEditar()
        {
            try
            {
                if (ArtistaEditar != null && ArtistaEditar.IdArtista > 0 && !string.IsNullOrWhiteSpace(ArtistaEditar.Nombre))
                {
                    // Obtener el estado actual del artista para no modificarlo
                    var artistaActual = _cnArtista.ObtenerDetalleArtista(ArtistaEditar.IdArtista);
                    ArtistaEditar.Estado = artistaActual.Estado;

                    _cnArtista.EditarArtista(ArtistaEditar);

                    Mensaje = "Artista editado exitosamente.";
                }
                else
                {
                    Mensaje = "Error: Datos inválidos para editar el artista.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al editar el artista: {ex.Message}";
            }

            return RedirectToPage("./CRUD_Artista");
        }



        public IActionResult OnPostEliminar()
        {
            try
            {
                if (IdArtistaEliminar.HasValue)
                {
                    _cnArtista.EliminarArtista(IdArtistaEliminar.Value);

                    Mensaje = "Artista eliminado exitosamente.";
                }
                else
                {
                    Mensaje = "Error: No se pudo identificar el artista a eliminar.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al eliminar el artista: {ex.Message}";
            }

            return RedirectToPage("./CRUD_Artista");
        }


        private void CargarDatos()
        {
            try
            {
                ListaArtistas = _cnArtista.ObtenerArtistas(); // Carga todos los artistas
                ListaRoles = _cnRoles.ListarRoles(out _);
                ListaEspecialidades = _cnEspecialidades.ListarEspecialidades(out _);

                foreach (var artista in ListaArtistas)
                {
                    // Obtener especialidades asignadas
                    artista.Especialidades = _cnArtista.ObtenerEspecialidadesPorArtista(artista.IdArtista)
                                                       .Select(e => e.NombreEspecialidad)
                                                       .ToList();

                    // Vincular roles
                    var rol = ListaRoles.FirstOrDefault(r => r.IdRole == artista.IdRole);
                    artista.Rol = rol?.Role ?? "Sin Rol";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al cargar los datos: {ex.Message}";
            }
        }

    }
}
