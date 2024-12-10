using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_MetodoDePago
    {
        public int IdMetodoPago { get; set; }
        public int IdCliente { get; set; }
        public int IdTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CVV { get; set; }
    }
}
