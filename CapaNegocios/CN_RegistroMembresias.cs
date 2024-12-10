using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class CN_RegistroMembresias
    {
        private CD_RegistroMembresias objCapaDatos = new CD_RegistroMembresias();

        public bool RegistrarCompraMembresia(E_RegistroMembresias registro, decimal? monto, string numeroDeTarjeta, out string mensaje)
        {
            // Validaciones básicas
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

            // Llamada a la capa de datos
            return objCapaDatos.RegistrarCompraMembresia(registro, monto, numeroDeTarjeta, out mensaje);
        }

        // Método para obtener la información de la membresía del cliente
        public E_RegistroMembresias ObtenerMembresiaCliente(int idCliente, out string mensaje)
        {
            if (idCliente <= 0)
            {
                mensaje = "El ID del cliente no es válido.";
                return null;
            }

            // Llamada a la capa de datos
            return objCapaDatos.ObtenerMembresiaCliente(idCliente, out mensaje);
        }
    }
}
