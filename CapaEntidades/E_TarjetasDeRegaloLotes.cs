using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_TarjetasDeRegaloLotes
    {
        public int IdLote { get; set; }
        public int Cantidad { get; set; }
        public decimal Valor { get; set; }
        public string Contenido { get; set; }
        public int CreadoPor { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public bool Estado { get; set; }
        public int AprobadoPor { get; set; }
    }
}
