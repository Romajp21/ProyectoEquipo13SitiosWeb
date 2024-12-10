using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_CategoriaPreguntasFrecuentes
    {
        private CD_CategoriaPreguntasFrecuentes objCapaDatos = new CD_CategoriaPreguntasFrecuentes();

        public List<E_CategoriaPreguntasFrecuentes> Listar(out string mensaje)
        {
            return objCapaDatos.Listar(out mensaje);
        }

        public bool Registrar(E_CategoriaPreguntasFrecuentes categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria.CategoriaPreguntasFrecuentes))
            {
                mensaje = "La categoría no puede estar vacía.";
                return false;
            }

            return objCapaDatos.Crear(categoria, out mensaje);
        }

        public bool Editar(E_CategoriaPreguntasFrecuentes categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria.CategoriaPreguntasFrecuentes))
            {
                mensaje = "La categoría no puede estar vacía.";
                return false;
            }

            return objCapaDatos.Actualizar(categoria, out mensaje);
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDatos.Eliminar(id, out mensaje);
        }
    }
}
