using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_AprobacionMembresiaContenido
    {
        private readonly CD_AprobacionMembresiaContenido _cdAprobacion = new CD_AprobacionMembresiaContenido();

        // Método para registrar una nueva aprobación
        public bool RegistrarAprobacion(E_AprobacionMembresiaContenido aprobacion, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio: verifica que IdMembresContenido y AprobadoPor no sean nulos o inválidos
            if (aprobacion.IdMembresContenido <= 0)
            {
                mensaje = "El ID del contenido de membresía es inválido.";
                return false;
            }

            if (aprobacion.AprobadoPor <= 0)
            {
                mensaje = "El ID del aprobador es inválido.";
                return false;
            }

            return _cdAprobacion.RegistrarAprobacion(aprobacion, out mensaje);
        }

        // Método para listar aprobaciones junto con los contenidos de membresía
        public List<E_AprobacionMembresiaContenido> ListarAprobaciones(int? idMembresia, int? idMembresContenido, out string mensaje)
        {
            // Llama al método de la capa de datos y devuelve los datos completos
            return _cdAprobacion.ListarAprobaciones(idMembresia, idMembresContenido, out mensaje);
        }

        // Método para actualizar una aprobación existente
        public bool ActualizarAprobacion(E_AprobacionMembresiaContenido aprobacion, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio: verifica que IdAprobacion y AprobadoPor sean válidos
            if (aprobacion.IdAprobacion == null || aprobacion.IdAprobacion <= 0)
            {
                mensaje = "El ID de la aprobación es inválido.";
                return false;
            }

            if (aprobacion.AprobadoPor <= 0)
            {
                mensaje = "El ID del aprobador es inválido.";
                return false;
            }

            return _cdAprobacion.ActualizarAprobacion(aprobacion, out mensaje);
        }

        // Método para eliminar una aprobación
        public bool EliminarAprobacion(int idAprobacion, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación de negocio: verifica que IdAprobacion sea válido
            if (idAprobacion <= 0)
            {
                mensaje = "El ID de la aprobación es inválido.";
                return false;
            }

            return _cdAprobacion.EliminarAprobacion(idAprobacion, out mensaje);
        }
    }
}
