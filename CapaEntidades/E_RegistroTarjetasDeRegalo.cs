using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_RegistroTarjetasDeRegalo
    {
        public int IdRegistroTarjetaDeRegalo { get; set; }
        public int IdCliente { get; set; }
        public int IdMembresia { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public bool Estado { get; set; }
    }
}
