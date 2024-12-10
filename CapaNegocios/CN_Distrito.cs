using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Distrito
    {
        private CD_Distrito objCapaDatos = new CD_Distrito();

        public List<E_Distrito> Listar(out string mensaje)
        {
            return objCapaDatos.Listar(out mensaje);
        }

        public bool Registrar(E_Distrito distrito, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(distrito.NombreDistrito))
            {
                mensaje = "El nombre del distrito no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Crear(distrito, out mensaje);
        }

        public bool Editar(E_Distrito distrito, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(distrito.NombreDistrito))
            {
                mensaje = "El nombre del distrito no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Actualizar(distrito, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDatos.Eliminar(id, out mensaje);
        }
    }
}
