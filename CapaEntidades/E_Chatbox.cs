namespace CapaEntidades
{
    // Representa una pregunta del chatbox
    public class E_Chatbox
    {
        public int IdPregunta { get; set; } // ID de la pregunta
        public string TextoPregunta { get; set; } // Texto de la pregunta
        public bool EstadoPregunta { get; set; } // Activa/Inactiva (Estado)

        // Lista de respuestas asociadas a la pregunta
        public List<E_ChatboxRespuesta> Respuestas { get; set; } = new List<E_ChatboxRespuesta>();

        // Propiedad para contar respuestas activas
        public int RespuestasActivas => Respuestas.Count(r => r.EstadoRespuesta);
    }

    // Representa una respuesta asociada a una pregunta
    public class E_ChatboxRespuesta
    {
        public int IdRespuesta { get; set; } // ID de la respuesta
        public string TextoRespuesta { get; set; } // Texto de la respuesta
        public int? IdPreguntaDestino { get; set; } // ID de la siguiente pregunta (puede ser NULL)
        public int Orden { get; set; } // Orden en que aparece la respuesta
        public bool EstadoRespuesta { get; set; } // Activa/Inactiva (Estado)
    }
}