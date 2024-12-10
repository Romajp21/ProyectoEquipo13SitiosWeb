using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Calendario
    {
        private CD_Calendario objCapaDatos = new CD_Calendario();

        public List<E_Calendario> ListarCalendario(out string mensaje)
        {
            return objCapaDatos.ListarCalendario(out mensaje);
        }

        public bool RegistrarBloque(E_Calendario bloque, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(bloque.DiaSemana))
            {
                mensaje = "El día de la semana no puede estar vacío.";
                return false;
            }

            return objCapaDatos.CrearBloque(bloque, out mensaje);
        }

        public bool EditarBloque(E_Calendario bloque, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(bloque.DiaSemana))
            {
                mensaje = "El día de la semana no puede estar vacío.";
                return false;
            }

            return objCapaDatos.ActualizarBloque(bloque, out mensaje);
        }

        public bool EliminarBloque(int idBloque, out string mensaje)
        {
            return objCapaDatos.EliminarBloque(idBloque, out mensaje);
        }
    }
}
