using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_PreguntasFrecuentes
    {
        public int IdPreguntas { get; set; }
        public int IdCategoriaPreguntas { get; set; }
        public int IdOrden { get; set; }

        public bool Estado { get; set; }
    }
}
