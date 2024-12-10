using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_TarjetaCliente
    {
        public int IdTarjeta { get; set; }
        public int IdCliente { get; set; }
        public decimal Monto { get; set; }
    }
}
