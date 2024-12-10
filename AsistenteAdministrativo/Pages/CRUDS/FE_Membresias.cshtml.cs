using CapaNegocios;
using CapaEntidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AsistenteAdministrativo.Pages.CRUDS
{
    public class FE_MembresiasModel : PageModel
    {
        private readonly CN_Membresias _cnMembresias;

        public FE_MembresiasModel(CN_Membresias cnMembresias)
        {
            _cnMembresias = cnMembresias;
            NuevaMembresia = new E_Membresias(); // Inicializar para evitar nulls
            Membresias = new List<E_Membresias>();
        }

        public List<E_Membresias> Membresias { get; set; }

        [BindProperty]
        public E_Membresias NuevaMembresia { get; set; }

        [TempData]
        public string Mensaje { get; set; }

        public void OnGet()
        {
            CargarMembresias();
        }

        // Método para crear o actualizar una membresía
        public IActionResult OnPostGuardar()
        {
            if (!ModelState.IsValid)
            {
                CargarMembresias();
                return Page();
            }

            string mensaje;
            bool exito;

            if (NuevaMembresia.IdMembresia == 0) // Crear
            {
                exito = _cnMembresias.RegistrarMembresia(NuevaMembresia, out mensaje);
            }
            else // Actualizar
            {
                exito = _cnMembresias.ActualizarMembresia(NuevaMembresia, out mensaje);
            }

            Mensaje = mensaje;
            if (exito)
            {
                return RedirectToPage(); // Redirigir para mostrar el mensaje en la recarga
            }

            CargarMembresias(); // Recargar membresías en caso de error
            return Page(); // Mostrar mensaje en la misma página
        }

        // Método para eliminar una membresía
        public IActionResult OnPostEliminar(int id)
        {
            string mensaje;

            if (id <= 0) // Validar ID
            {
                TempData["Mensaje"] = "El ID de la membresía no es válido para eliminar.";
                return RedirectToPage();
            }

            bool exito = _cnMembresias.EliminarMembresia(id, out mensaje);
            TempData["Mensaje"] = mensaje; // Asignar mensaje relevante

            return RedirectToPage(); // Siempre redirigir después de manejar una acción
        }





        // Método para cargar una membresía específica en el formulario para actualizarla
        public IActionResult OnGetCargarParaActualizar(int id)
        {
            string mensaje;
            var membresia = _cnMembresias.ObtenerMembresia(id, out mensaje);

            if (membresia == null)
            {
                return new JsonResult(new { error = mensaje });
            }

            return new JsonResult(new
            {
                idMembresia = membresia.IdMembresia,
                nombreMembresia = membresia.NombreMembresia,
                precio = membresia.Precio,
                duracion = membresia.Duracion,
                estado = membresia.Estado,
                creadoPor = membresia.CreadoPor
            });
        }


        private void CargarMembresias()
        {
            Membresias = _cnMembresias.ListarMembresias(out _); // Ignorar mensajes
        }



    }
}
