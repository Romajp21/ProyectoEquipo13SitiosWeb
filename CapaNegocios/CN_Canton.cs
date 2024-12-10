using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Canton
    {
        private CD_Canton objCapaDatos = new CD_Canton();

        public List<E_Canton> Listar(out string mensaje)
        {
            return objCapaDatos.Listar(out mensaje);
        }

        public bool Registrar(E_Canton canton, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(canton.NombreCanton))
            {
                mensaje = "El nombre del cantón no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Crear(canton, out mensaje);
        }

        public bool Editar(E_Canton canton, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(canton.NombreCanton))
            {
                mensaje = "El nombre del cantón no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Actualizar(canton, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDatos.Eliminar(id, out mensaje);
        }
    }
}
