using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Membresias
    {
        private CD_Membresias objCapaDatos = new CD_Membresias();

        // Método para listar todas las membresías
        public List<E_Membresias> ListarMembresias(out string mensaje)
        {
            return objCapaDatos.ListarMembresias(out mensaje);
        }

        // Método para registrar una nueva membresía
        public bool RegistrarMembresia(E_Membresias membresia, out string mensaje)
        {
            if (!ValidarMembresia(membresia, out mensaje))
                return false;

            return objCapaDatos.RegistrarMembresia(membresia, out mensaje);
        }

        // Método para actualizar una membresía
        public bool ActualizarMembresia(E_Membresias membresia, out string mensaje)
        {
            if (!ValidarMembresia(membresia, out mensaje))
                return false;

            return objCapaDatos.ActualizarMembresia(membresia, out mensaje);
        }

        // Método para eliminar una membresía
        public bool EliminarMembresia(int id, out string mensaje)
        {
            mensaje = string.Empty;
            if (id <= 0)
            {
                mensaje = "El ID de la membresía no es válido.";
                return false;
            }

            return objCapaDatos.EliminarMembresia(id, out mensaje);
        }

        // Método para obtener una membresía específica por su ID
        public E_Membresias ObtenerMembresia(int id, out string mensaje)
        {
            return objCapaDatos.ObtenerMembresia(id, out mensaje);
        }

        // Método privado para validar los datos de la membresía
        private bool ValidarMembresia(E_Membresias membresia, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(membresia.NombreMembresia))
            {
                mensaje = "El nombre de la membresía no puede estar vacío.";
                return false;
            }

            if (membresia.Precio <= 0)
            {
                mensaje = "El precio de la membresía debe ser mayor a cero.";
                return false;
            }

            if (membresia.Duracion <= 0)
            {
                mensaje = "La duración de la membresía debe ser mayor a cero.";
                return false;
            }

            return true;
        }

        // Método para registrar la compra de una membresía
        public bool RegistrarCompraMembresia(E_RegistroMembresias registro, E_PagoMembresias pago, out string mensaje)
        {
            if (!ValidarCompraMembresia(registro, pago, out mensaje))
                return false;

            return objCapaDatos.RegistrarCompraMembresia(registro, pago, out mensaje);
        }

        // Método privado para validar la compra de una membresía
        private bool ValidarCompraMembresia(E_RegistroMembresias registro, E_PagoMembresias pago, out string mensaje)
        {
            mensaje = string.Empty;

            if (registro.IdCliente <= 0)
            {
                mensaje = "El ID del cliente no es válido.";
                return false;
            }

            if (registro.IdMembresia <= 0)
            {
                mensaje = "El ID de la membresía no es válido.";
                return false;
            }

            if (pago.Monto <= 0)
            {
                mensaje = "El monto del pago debe ser mayor a cero.";
                return false;
            }

            if (string.IsNullOrEmpty(pago.NumeroDeTarjeta))
            {
                mensaje = "El número de tarjeta no puede estar vacío.";
                return false;
            }

            return true;
        }
    }
}
