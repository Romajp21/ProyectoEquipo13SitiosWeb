using CapaDatos;
using System.Collections.Generic;
using CapaEntidades;
 

namespace CapaNegocios
{
    public class ProductoNegocios
    {
        private readonly ProductoDatos _productoDatos;

        public ProductoNegocios(ProductoDatos productoDatos)
        {
            _productoDatos = productoDatos;
        }

        public List<E_Producto> ObtenerProductos()
        {
            return _productoDatos.ListarProductos();
        }



    }
}
