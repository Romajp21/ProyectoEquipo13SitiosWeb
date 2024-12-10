using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Respuestas
    {
        public int IdRespuesta { get; set; }
        public int IdPreguntas { get; set; }
        public string Respuesta { get; set; }
        public int IdOrden { get; set; }
        public bool Estado { get; set; }
    }
}
