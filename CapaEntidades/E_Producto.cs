using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string CategoriaProducto { get; set; }
        public string ImagenUrl { get; set; }
        public bool Estado { get; set; }
    }
}
