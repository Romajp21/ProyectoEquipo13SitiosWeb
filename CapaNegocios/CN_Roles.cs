using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Roles
    {
        private CD_Roles objCapaDatos = new CD_Roles();

        public List<E_Roles> ListarRoles(out string mensaje)
        {
            return objCapaDatos.ListarRoles(out mensaje);
        }

        // Crear un nuevo rol
        public bool RegistrarRol(E_Roles rol, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio
            if (string.IsNullOrEmpty(rol.Role) || string.IsNullOrWhiteSpace(rol.Role))
            {
                mensaje = "El nombre del rol no puede estar vacío.";
                return false;
            }

            // Llamada al método de capa de datos para registrar
            return objCapaDatos.CrearRol(rol, out mensaje);
        }

        // Actualizar un rol existente
        public bool EditarRol(E_Roles rol, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio
            if (string.IsNullOrEmpty(rol.Role) || string.IsNullOrWhiteSpace(rol.Role))
            {
                mensaje = "El nombre del rol no puede estar vacío.";
                return false;
            }

            // Llamada al método de capa de datos para actualizar
            return objCapaDatos.ActualizarRol(rol, out mensaje);
        }

        // Eliminar un rol por su ID
        public bool EliminarRol(int idRole, out string mensaje)
        {
            // Llamada al método de capa de datos para eliminar
            return objCapaDatos.EliminarRol(idRole, out mensaje);
        }
    }
}
