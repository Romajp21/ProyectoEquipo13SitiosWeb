using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_TipoDeEmpleado
    {
        private CD_TipoDeEmpleado objCapaDatos = new CD_TipoDeEmpleado();

        public List<E_TipoDeEmpleado> ListarTipoDeEmpleado(out string mensaje)
        {
            return objCapaDatos.ListarTipoDeEmpleado(out mensaje);
        }

        public bool RegistrarTipoDeEmpleado(E_TipoDeEmpleado tipoEmpleado, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(tipoEmpleado.TipoDeEmpleado) || string.IsNullOrWhiteSpace(tipoEmpleado.TipoDeEmpleado))
            {
                mensaje = "El tipo de empleado no puede estar vacío.";
                return false;
            }

            return objCapaDatos.CrearTipoDeEmpleado(tipoEmpleado, out mensaje);
        }

        public bool EditarTipoDeEmpleado(E_TipoDeEmpleado tipoEmpleado, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(tipoEmpleado.TipoDeEmpleado) || string.IsNullOrWhiteSpace(tipoEmpleado.TipoDeEmpleado))
            {
                mensaje = "El tipo de empleado no puede estar vacío.";
                return false;
            }

            return objCapaDatos.ActualizarTipoDeEmpleado(tipoEmpleado, out mensaje);
        }

        public bool EliminarTipoDeEmpleado(int idTipodeEmpleado, out string mensaje)
        {
            return objCapaDatos.EliminarTipoDeEmpleado(idTipodeEmpleado, out mensaje);
        }
    }
}
