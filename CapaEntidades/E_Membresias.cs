using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Membresias
    {
        public int IdMembresia { get; set; }
        public string NombreMembresia { get; set; }
        public decimal Precio { get; set; }
        public int Duracion { get; set; }
        public int CreadoPor { get; set; }
        public bool Estado { get; set; }
    }
}
