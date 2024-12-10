using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_PagoTarjetasDeRegalo
    {
        public int IdPago { get; set; }
        public int IdRegistroTarjetaDeRegalo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string NumeroDeTarjeta { get; set; }
        public bool Estado { get; set; }
        public string ReferenciaPago { get; set; }
    }
}
