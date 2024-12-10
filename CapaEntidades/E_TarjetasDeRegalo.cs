using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_TarjetasDeRegalo
    {
        public int IDTarjetaDeRegalo { get; set; }
        public int IdLote { get; set; }
        public bool Estado { get; set; }
        public int CreadoPor { get; set; }
        public string Comentario { get; set; }
        public int AprobadoPor { get; set; }
    }
}
