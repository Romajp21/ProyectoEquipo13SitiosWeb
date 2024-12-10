using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Tamanos
    {
        private CD_Tamanos objCapaDatos = new CD_Tamanos();

        public List<E_Tamanos> ListarTamanos(out string mensaje)
        {
            return objCapaDatos.ListarTamanos(out mensaje);
        }

        public bool RegistrarTamano(E_Tamanos tamano, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(tamano.Tamano) || string.IsNullOrWhiteSpace(tamano.Tamano))
            {
                mensaje = "El nombre del tamaño no puede estar vacío.";
                return false;
            }

            return objCapaDatos.CrearTamano(tamano, out mensaje);
        }

        public bool EditarTamano(E_Tamanos tamano, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(tamano.Tamano) || string.IsNullOrWhiteSpace(tamano.Tamano))
            {
                mensaje = "El nombre del tamaño no puede estar vacío.";
                return false;
            }

            return objCapaDatos.ActualizarTamano(tamano, out mensaje);
        }

        public bool EliminarTamano(int idTamano, out string mensaje)
        {
            return objCapaDatos.EliminarTamano(idTamano, out mensaje);
        }
    }
}
