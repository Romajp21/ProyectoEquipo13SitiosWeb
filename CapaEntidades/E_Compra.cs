using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Compra
    {
        public int IdCompra { get; set; }
        public int IdCliente { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string IdTransaccion { get; set; }
    }
}
