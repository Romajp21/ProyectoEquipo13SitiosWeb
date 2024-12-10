using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_MembresiaContenido
    {
        private readonly CD_MembresiaContenido _cdMembresiaContenido = new CD_MembresiaContenido();

        // Método para listar contenidos de una membresía
        public List<E_MembresiaContenido> ListarContenido(int? idMembresia, out string mensaje)
        {
            return _cdMembresiaContenido.ListarContenido(idMembresia, out mensaje);
        }

        // Método para registrar un nuevo contenido para una membresía
        public bool RegistrarContenido(E_MembresiaContenido contenido, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio
            if (string.IsNullOrEmpty(contenido.NombreContenido))
            {
                mensaje = "El nombre del contenido no puede estar vacío.";
                return false;
            }

            // Validación del campo Estado
            if (contenido.Estado != true && contenido.Estado != false)
            {
                mensaje = "El estado debe ser Activo o Inactivo.";
                return false;
            }

            return _cdMembresiaContenido.RegistrarContenido(contenido, out mensaje);
        }

        // Método para actualizar un contenido de una membresía
        public bool ActualizarContenido(E_MembresiaContenido contenido, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio
            if (string.IsNullOrEmpty(contenido.NombreContenido))
            {
                mensaje = "El nombre del contenido no puede estar vacío.";
                return false;
            }

            // Validación del campo Estado
            if (contenido.Estado != true && contenido.Estado != false)
            {
                mensaje = "El estado debe ser Activo o Inactivo.";
                return false;
            }

            return _cdMembresiaContenido.ActualizarContenido(contenido, out mensaje);
        }

        // Método para eliminar un contenido de una membresía
        public bool EliminarContenido(int idMembresContenido, out string mensaje)
        {
            return _cdMembresiaContenido.EliminarContenido(idMembresContenido, out mensaje);
        }
    }
}
