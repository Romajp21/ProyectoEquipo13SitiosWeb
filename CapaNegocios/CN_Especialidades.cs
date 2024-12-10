using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Especialidades
    {
        private CD_Especialidades objCapaDatos = new CD_Especialidades();

        public List<E_Especialidades> ListarEspecialidades(out string mensaje)
        {
            return objCapaDatos.ListarEspecialidades(out mensaje);
        }

        public bool RegistrarEspecialidad(E_Especialidades especialidad, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(especialidad.NombreEspecialidad) || string.IsNullOrWhiteSpace(especialidad.NombreEspecialidad))
            {
                mensaje = "El nombre de la especialidad no puede estar vacío.";
                return false;
            }

            return objCapaDatos.RegistrarEspecialidad(especialidad, out mensaje);
        }

        public bool EditarEspecialidad(E_Especialidades especialidad, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(especialidad.NombreEspecialidad) || string.IsNullOrWhiteSpace(especialidad.NombreEspecialidad))
            {
                mensaje = "El nombre de la especialidad no puede estar vacío.";
                return false;
            }

            return objCapaDatos.ActualizarEspecialidad(especialidad, out mensaje);
        }

        public bool EliminarEspecialidad(int idEspecialidad, out string mensaje)
        {
            return objCapaDatos.EliminarEspecialidad(idEspecialidad, out mensaje);
        }
    }
}
