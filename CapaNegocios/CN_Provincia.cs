using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Provincia
    {
        private CD_Provincia objCapaDatos = new CD_Provincia();

        public List<E_Provincia> Listar(out string mensaje)
        {
            return objCapaDatos.Listar(out mensaje);
        }

        public bool Registrar(E_Provincia provincia, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(provincia.NombreProvincia))
            {
                mensaje = "El nombre de la provincia no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Crear(provincia, out mensaje);
        }

        public bool Editar(E_Provincia provincia, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(provincia.NombreProvincia))
            {
                mensaje = "El nombre de la provincia no puede estar vacío.";
                return false;
            }

            return objCapaDatos.Actualizar(provincia, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDatos.Eliminar(id, out mensaje);
        }
    }
}
