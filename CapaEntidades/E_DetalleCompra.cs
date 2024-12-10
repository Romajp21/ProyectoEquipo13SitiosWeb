using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
