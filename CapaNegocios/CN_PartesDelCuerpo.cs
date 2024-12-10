using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_PartesDelCuerpo
    {
        private CD_PartesDelCuerpo objCapaDatos = new CD_PartesDelCuerpo();

        public List<E_PartesDelCuerpo> ListarPartesDelCuerpo(out string mensaje)
        {
            return objCapaDatos.ListarPartesDelCuerpo(out mensaje);
        }

        public bool RegistrarParteDelCuerpo(E_PartesDelCuerpo parte, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(parte.ParteDelCuerpo) || string.IsNullOrWhiteSpace(parte.ParteDelCuerpo))
            {
                mensaje = "El nombre de la parte del cuerpo no puede estar vacío.";
                return false;
            }

            return objCapaDatos.CrearParteDelCuerpo(parte, out mensaje);
        }

        public bool EditarParteDelCuerpo(E_PartesDelCuerpo parte, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(parte.ParteDelCuerpo) || string.IsNullOrWhiteSpace(parte.ParteDelCuerpo))
            {
                mensaje = "El nombre de la parte del cuerpo no puede estar vacío.";
                return false;
            }

            return objCapaDatos.ActualizarParteDelCuerpo(parte, out mensaje);
        }

        public bool EliminarParteDelCuerpo(int idParteDelCuerpo, out string mensaje)
        {
            return objCapaDatos.EliminarParteDelCuerpo(idParteDelCuerpo, out mensaje);
        }
    }
}
