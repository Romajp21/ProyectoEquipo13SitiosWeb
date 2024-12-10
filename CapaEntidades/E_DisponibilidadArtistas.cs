using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_DisponibilidadArtistas
    {
        public int IdDisponibilidad { get; set; }
        public int IdArtista { get; set; }
        public DateOnly Fecha { get; set; }
        public int IdBloque { get; set; }
        public bool Estado { get; set; }
    }
}
