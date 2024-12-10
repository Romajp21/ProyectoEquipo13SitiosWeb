using CapaDatos;
using CapaEntidades;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Productos
    {
        private readonly CD_Productos _cdProductos;

        public CN_Productos(CD_Productos cdProductos)
        {
            _cdProductos = cdProductos;
        }

        public List<E_Producto> ObtenerProductosConDetalles()
        {
            return _cdProductos.ObtenerProductosConDetalles();
        }

        public void CambiarEstadoProducto(int idProducto, bool nuevoEstado)
        {
            _cdProductos.CambiarEstadoProducto(idProducto, nuevoEstado);
        }
    }
}
