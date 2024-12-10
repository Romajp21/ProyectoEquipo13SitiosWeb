using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public  class E_RecargaTarjeta
    {
        public int IdRecarga { get; set; }
        public int IdTarjeta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
