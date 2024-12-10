using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_CarritoCompras
    {
        public int IdCarrito { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
