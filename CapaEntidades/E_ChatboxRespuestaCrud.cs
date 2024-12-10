using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_ChatboxRespuestaCrud
    {
        public int IdRespuesta { get; set; } // Identificador único de la respuesta
        public int IdPregunta { get; set; } // ID de la pregunta asociada
        public string TextoRespuesta { get; set; } // Texto de la respuesta
        public int? IdPreguntaDestino { get; set; } // ID de la pregunta destino (opcional)
        public int Orden { get; set; } // Orden de la respuesta en la lista
        public bool Estado { get; set; } // Estado de la respuesta (activa/inactiva)

        // Propiedad adicional para mostrar el texto de la pregunta asociada (útil para listados)
       
        public string TextoPregunta { get; set; }
    }
}
