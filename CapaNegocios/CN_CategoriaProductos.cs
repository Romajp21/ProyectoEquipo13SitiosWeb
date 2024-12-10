using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_CategoriaProductos
    {
        private CD_CategoriaProductos objCapaDatos = new CD_CategoriaProductos();

        public List<E_CategoriaProductos> Listar(out string mensaje)
        {
            return objCapaDatos.Listar(out mensaje);
        }

        public bool Registrar(E_CategoriaProductos categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria.CategoriaProducto))
            {
                mensaje = "La categoría no puede estar vacía.";
                return false;
            }

            return objCapaDatos.Crear(categoria, out mensaje);
        }

        public bool Editar(E_CategoriaProductos categoria, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria.CategoriaProducto))
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
