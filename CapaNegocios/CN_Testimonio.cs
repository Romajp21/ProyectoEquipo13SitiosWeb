using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Testimonio
    {
        // Instancia de la capa de datos para interactuar con la base de datos
        private readonly CD_Testimonio objCapaDatos = new CD_Testimonio();

        // Listar citas sin testimonios para un cliente
        public List<E_Citas> ListarCitasSinTestimonios(int idCliente, out string mensaje)
        {
            return objCapaDatos.ListarCitasSinTestimonios(idCliente, out mensaje);
        }

        // Registrar un nuevo testimonio
        public bool RegistrarTestimonio(E_Testimonio testimonio, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar los datos del testimonio
            if (!ValidarParaRegistrar(testimonio, out mensaje))
            {
                return false;
            }

            // Llamar al método en la capa de datos
            return objCapaDatos.RegistrarTestimonio(testimonio, out mensaje);
        }

        // Aprobar o rechazar un testimonio
        public bool AprobarTestimonio(int idTestimonio, bool estado, int revisadoPor, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar que el ID del testimonio sea válido
            if (idTestimonio <= 0)
            {
                mensaje = "El ID del testimonio no es válido.";
                return false;
            }

            // Validar que el ID del empleado sea válido
            if (revisadoPor <= 0)
            {
                mensaje = "El ID del revisor no es válido.";
                return false;
            }

            // Llamar al método en la capa de datos
            return objCapaDatos.AprobarTestimonio(idTestimonio, estado, revisadoPor, out mensaje);
        }

        // Método privado para validar los datos del testimonio al registrar
        private bool ValidarParaRegistrar(E_Testimonio testimonio, out string mensaje)
        {
            mensaje = string.Empty;

            // Validar el ID del cliente
            if (testimonio.IdCliente <= 0)
            {
                mensaje = "El ID del cliente no es válido.";
                return false;
            }

            // Validar el ID de la cita
            if (testimonio.IdCita <= 0)
            {
                mensaje = "El ID de la cita no es válido.";
                return false;
            }

            // Validar que el testimonio no esté vacío
            if (string.IsNullOrEmpty(testimonio.Testimonio))
            {
                mensaje = "El testimonio no puede estar vacío.";
                return false;
            }

            // Validar que la calificación esté dentro del rango permitido
            if (testimonio.Calificacion < 1 || testimonio.Calificacion > 5)
            {
                mensaje = "La calificación debe estar entre 1 y 5.";
                return false;
            }

            return true;
        }

        // Obtener testimonios pendientes de aprobación
        public List<E_Testimonio> ObtenerTestimoniosPendientes(out string mensaje)
        {
            return objCapaDatos.ListarTestimoniosPendientes(out mensaje);
        }

        // Aprobar o rechazar un testimonio
        public bool AprobarRechazarTestimonio(int idTestimonio, int revisadoPor, bool estado, out string mensaje)
        {
            // Validación simple antes de enviar a la capa de datos
            if (idTestimonio <= 0)
            {
                mensaje = "El ID del testimonio no es válido.";
                return false;
            }

            return objCapaDatos.AprobarRechazarTestimonio(idTestimonio, revisadoPor, estado, out mensaje);
        }
        public List<E_Testimonio> ObtenerTestimoniosAprobados(int cantidad, out string mensaje)
        {
            if (cantidad <= 0) cantidad = 5; // Por defecto, mostrar 5 testimonios
            return objCapaDatos.ListarTestimoniosAprobados(cantidad, out mensaje);
        }


    }
}
